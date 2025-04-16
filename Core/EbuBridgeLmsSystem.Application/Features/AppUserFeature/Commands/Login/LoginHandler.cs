using EbuBridgeLmsSystem.Application.Dtos.Auth;
using EbuBridgeLmsSystem.Application.Interfaces;
using EbuBridgeLmsSystem.Application.Settings;
using EbuBridgeLmsSystem.Domain.Entities;
using EbuBridgeLmsSystem.Domain.Entities.Common;
using EbuBridgeLmsSystem.Domain.Repositories;
using Hangfire;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Stripe.Forwarding;

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
        private readonly SignInManager<AppUser> _signInManager;
        public LoginHandler(IOptions<JwtSettings> jwtSettings, UserManager<AppUser> userManager, IEmailService emailService, ITokenService tokenService, IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _emailService = emailService;
            _jwtSettings = jwtSettings.Value;
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
            _contextAccessor = contextAccessor;
            _signInManager = signInManager;
        }

        public async Task<Result<AuthResponseDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await FindUserAsync(request.UserNameOrGmail);
            if (user == null)
            {
                return Result<AuthResponseDto>.Failure(Error.Custom("UserNameOrGmail", "userName or email is wrong\""), null, ErrorType.NotFoundError);
            }
             await HandleFirstTimeLogin(user);
             var result = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!result)
            {
                return Result<AuthResponseDto>.Failure(Error.Custom("Password", "Password or email is wrong\""), null, ErrorType.ValidationError);
            }
            var lockOutResult = await CheckLockOutStatus(user, request.Password);
            if (!lockOutResult.IsSuccess)
            {
                return Result<AuthResponseDto>.Failure(lockOutResult.Error, lockOutResult.Errors, (ErrorType)lockOutResult.ErrorType);
            }
            var IsUserBlockedResult=  await HandleUserBlockSituation(user);
            if (!IsUserBlockedResult.IsSuccess)
            {
                return Result<AuthResponseDto>.Failure(IsUserBlockedResult.Error, IsUserBlockedResult.Errors, (ErrorType)IsUserBlockedResult.ErrorType);
            }
            var deleteUserRecoveryResult = CheckAccountRecovery(user);
            if (!deleteUserRecoveryResult.IsSuccess)
            {
                return Result<AuthResponseDto>.Failure(deleteUserRecoveryResult.Error, deleteUserRecoveryResult.Errors, (ErrorType)deleteUserRecoveryResult.ErrorType);
            }

            IList<string> roles = await _userManager.GetRolesAsync(user);
            if (user.IsReportedHighly)
            {
                return Result<AuthResponseDto>.Failure(Error.Custom("User", "You are reported too many times ,so account is locked now, we will contact with you"), null, ErrorType.BusinessLogicError);
            }
            if (!user.IsEmailVerificationCodeValid) return Result<AuthResponseDto>.Failure(Error.Custom("User", "pls verify your account by getting code"), null, ErrorType.BusinessLogicError);
            var Audience = _jwtSettings.Audience;
            var SecretKey = _jwtSettings.secretKey;
            var Issuer = _jwtSettings.Issuer;
            var refreshTokenGenerated = _tokenService.GenerateRefreshToken();
            var existingToken = await _unitOfWork.RefreshTokenRepository.GetEntity(s => s.AppUserId == user.Id&&!s.IsDeleted);
            if (existingToken != null)
            {
                existingToken.Token = refreshTokenGenerated;
                existingToken.Expires = DateTime.UtcNow.AddDays(7);
                await existingToken.UpdateStatus();
                await _unitOfWork.RefreshTokenRepository.Update(existingToken);

            }
            else
            {
                RefreshToken refreshToken = new RefreshToken { AppUser = user, AppUserId = user.Id, Token = refreshTokenGenerated, Expires = DateTime.UtcNow.AddDays(7) };
                await refreshToken.UpdateStatus();
                await _unitOfWork.RefreshTokenRepository.Create(refreshToken);
            }
            _contextAccessor.HttpContext?.Response.Cookies.Delete("refreshToken");
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
            },null);
        }
        private async Task<AppUser> FindUserAsync(string userNameOrEmail)
        {
            return await _userManager.Users
                .Include(u => u.Student)
                .FirstOrDefaultAsync(u => u.Email.ToLower() == userNameOrEmail.ToLower() ||
                                   u.UserName.ToLower() == userNameOrEmail.ToLower());
        }
        private async Task HandleFirstTimeLogin(AppUser user)
        {
            if (user.IsFirstTimeLogined)
            {
                user.IsFirstTimeLogined = false;
                await _userManager.UpdateAsync(user);
                var body = "<h1>Welcome!</h1><p>Thank you for joining us. We're excited to have you!</p>";
                BackgroundJob.Enqueue(() => _emailService.SendEmailAsync(user.Email, "first time login", body, true));
            }
        }
        private async Task<Result<AuthResponseDto>> HandleUserBlockSituation(AppUser user)
        {
            if (user.IsBlocked && user.BlockedUntil.HasValue)
            {
                if (user.BlockedUntil.Value <= DateTime.UtcNow)
                {
                    user.IsBlocked = false;
                    user.BlockedUntil = null;
                    await _userManager.UpdateAsync(user);
                    return Result<AuthResponseDto>.Success(null,null);
                }
                else
                {

                    return Result<AuthResponseDto>.Failure(Error.Custom("UserNameOrGmail", $"you are blocked until {user.BlockedUntil?.ToString("dd MMM yyyy hh:mm")}"), null, ErrorType.BusinessLogicError);
                }
            }
            return Result<AuthResponseDto>.Success(null,null);

        }
        private Result<string> CheckAccountRecovery(AppUser user)
        {
            var deletedDate = user.DeletedTime;
            if (deletedDate is not null)
            {
                var today = DateTime.Now;
                var diffOfDates = today.Subtract((DateTime)deletedDate);
                if (diffOfDates.TotalDays >= 7)
                {
                    return Result<string>.Failure(Error.Custom("Error Login", "you lost the chance of getting your account back becuase 7 days already passed"), null, ErrorType.BusinessLogicError);
                }
            }
            return Result<string>.Success(null, null);
        }
        private async Task<Result<bool>> CheckLockOutStatus(AppUser user,string password)
        {
            SignInResult signInResult = await _signInManager.CheckPasswordSignInAsync(user, password, true);

            if (signInResult.IsLockedOut)
            {
                TimeSpan? timeSpan = user.LockoutEnd - DateTime.UtcNow;
                if (timeSpan is not null)
                    return Result<bool>.Failure(Error.Custom(null,$"Şifrenizi 5 defa yanlış girdiğiniz için kullanıcı {Math.Ceiling(timeSpan.Value.TotalMinutes)} dakika süreyle bloke edilmiştir"),null,ErrorType.BusinessLogicError);
                else
                    return Result<bool>.Failure(Error.Custom(null, $"Şifrenizi 5 defa yanlış girdiğiniz için kullanıcı {Math.Ceiling(timeSpan.Value.TotalMinutes)} dakika süreyle bloke edilmiştir"), null, ErrorType.BusinessLogicError);
            }
            return Result<bool>.Success(true, null);
        }
    }

}