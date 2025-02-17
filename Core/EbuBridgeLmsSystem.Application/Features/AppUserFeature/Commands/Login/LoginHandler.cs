using EbuBridgeLmsSystem.Application.Dtos.Auth;
using EbuBridgeLmsSystem.Application.Interfaces;
using EbuBridgeLmsSystem.Application.Settings;
using EbuBridgeLmsSystem.Domain.Entities;
using Hangfire;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
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
        public LoginHandler(IOptions<JwtSettings> jwtSettings, UserManager<AppUser> userManager, IEmailService emailService, ITokenService tokenService)
        {
            _userManager = userManager;
            _emailService = emailService;
            _jwtSettings = jwtSettings.Value;
            _tokenService = tokenService;
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
            IList<string> roles = await _userManager.GetRolesAsync(user);
            if (user.IsReportedHighly)
            {
                return Result<AuthResponseDto>.Failure("User", "You are reported too many times ,so account is locked now, we will contact with you", null, ErrorType.BusinessLogicError);
            }
            if (!user.IsEmailVerificationCodeValid) return Result<AuthResponseDto>.Failure("User", "pls verify your account by getting code", null, ErrorType.BusinessLogicError);
            var Audience = _jwtSettings.Audience;
            var SecretKey = _jwtSettings.secretKey;
            var Issuer = _jwtSettings.Issuer;
            return Result<AuthResponseDto>.Success(new AuthResponseDto
            {
                IsSuccess = true,
                Token = _tokenService.GetToken(SecretKey, Audience, Issuer, user, roles)
            });
        }
    }
}
