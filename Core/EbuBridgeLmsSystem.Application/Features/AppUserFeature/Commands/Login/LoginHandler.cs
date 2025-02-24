using EbuBridgeLmsSystem.Application.Dtos.Auth;
using EbuBridgeLmsSystem.Application.Interfaces;
using EbuBridgeLmsSystem.Application.Settings;
using EbuBridgeLmsSystem.Domain.Entities;
using EbuBridgeLmsSystem.Domain.Repositories;
using Hangfire;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace EbuBridgeLmsSystem.Application.Features.AppUserFeature.Commands.Login
{
    public class LoginHandler : IRequestHandler<LoginCommand, Result<AuthResponseDto>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly JwtSettings _jwtSettings;
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _contextAccessor;
        public LoginHandler(IOptions<JwtSettings> jwtSettings, UserManager<AppUser> userManager, IEmailService emailService, ITokenService tokenService, IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
        {
            _userManager = userManager;
            _emailService = emailService;
            _jwtSettings = jwtSettings.Value;
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
            _contextAccessor = contextAccessor;
        }

        public async Task<Result<AuthResponseDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users.Include(s => s.Student).
               FirstOrDefaultAsync(s => s.Email.ToLower() == request.UserNameOrGmail.ToLower());
            if (user == null)
            {
                user = await _userManager.Users
                    .Include(s => s.Student)
                    .FirstOrDefaultAsync(s => s.UserName.ToLower() == request.UserNameOrGmail.ToLower());
                if (user == null)
                {
                    return Result<AuthResponseDto>.Failure("UserNameOrGmail", "userName or email is wrong\"", null, ErrorType.NotFoundError);
                }
            }
            if (user.IsFirstTimeLogined)
            {
                user.IsFirstTimeLogined = false;
                await _userManager.UpdateAsync(user);
                var body = "<h1>Welcome!</h1><p>Thank you for joining us. We're excited to have you!</p>";
                BackgroundJob.Enqueue(() => _emailService.SendEmailAsync(user.Email, "first time login", body, true));
            }
            var result = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!result)
            {
                return Result<AuthResponseDto>.Failure("Password", "Password or email is wrong\"", null, ErrorType.ValidationError);
            }
            if (user.IsBlocked && user.BlockedUntil.HasValue)
            {
                if (user.BlockedUntil.Value <= DateTime.UtcNow)
                {
                    user.IsBlocked = false;
                    user.BlockedUntil = null;
                    await _userManager.UpdateAsync(user);
                }
                else
                {

                    return Result<AuthResponseDto>.Failure("UserNameOrGmail", $"you are blocked until {user.BlockedUntil?.ToString("dd MMM yyyy hh:mm")}", null, ErrorType.BusinessLogicError);
                }
            }
            var deletedDate = user.DeletedTime;
            if (deletedDate is not null)
            {
                var today = DateTime.Now;
                var diffOfDates = today.Subtract((DateTime)deletedDate);
                if (diffOfDates.TotalDays >= 7)
                {
                    return Result<AuthResponseDto>.Failure("Error Login", "you lost the chance of getting your account back becuase 7 days already passed", null, ErrorType.BusinessLogicError);
                }
            }
           
            IList<string> roles = await _userManager.GetRolesAsync(user);
            if (user.IsReportedHighly)
            {
                return Result<AuthResponseDto>.Failure("User", "You are reported too many times ,so account is locked now, we will contact with you", null, ErrorType.BusinessLogicError);
            }
            if (!user.IsEmailVerificationCodeValid) return Result<AuthResponseDto>.Failure("User", "pls verify your account by getting code", null, ErrorType.BusinessLogicError);
            var Audience = _jwtSettings.Audience;
            var SecretKey = _jwtSettings.secretKey;
            var Issuer = _jwtSettings.Issuer;
            var refreshTokenGenerated = _tokenService.GenerateRefreshToken();
            var existingToken = await _unitOfWork.RefreshTokenRepository.GetEntity(s => s.AppUserId == user.Id);
            if (existingToken != null)
            {
                await _unitOfWork.RefreshTokenRepository.Delete(existingToken);

            }
            RefreshToken refreshToken = new RefreshToken { AppUser = user, AppUserId = user.Id, Token = refreshTokenGenerated, Expires = DateTime.UtcNow.AddDays(7) };
            await refreshToken.UpdateStatus();
            await _unitOfWork.RefreshTokenRepository.Create(refreshToken);
            
            _contextAccessor.HttpContext?.Response.Cookies.Append("refreshToken", refreshTokenGenerated, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(7)
            });
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result<AuthResponseDto>.Success(new AuthResponseDto
            {
                IsSuccess = true,
                Token = _tokenService.GetToken(SecretKey, Audience, Issuer, user, roles)
            });
        }
    }
}
