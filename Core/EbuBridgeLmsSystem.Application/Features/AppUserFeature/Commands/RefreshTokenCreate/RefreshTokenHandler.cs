using EbuBridgeLmsSystem.Application.Dtos.Auth;
using EbuBridgeLmsSystem.Application.Interfaces;
using EbuBridgeLmsSystem.Application.Settings;
using EbuBridgeLmsSystem.Domain.Entities;
using EbuBridgeLmsSystem.Domain.Entities.Common;
using EbuBridgeLmsSystem.Domain.Repositories;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EbuBridgeLmsSystem.Application.Features.AppUserFeature.Commands.RefreshTokenCreate
{
    public class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, Result<AuthRefreshTokenResponseDto>>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;
        private readonly UserManager<AppUser> _userManager;
        private readonly JwtSettings _jwtSettings;
        private readonly ILogger<RefreshTokenHandler> _logger;
        public RefreshTokenHandler(IOptions<JwtSettings> jwtSettings, IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork, ITokenService tokenService, UserManager<AppUser> userManager, ILogger<RefreshTokenHandler> logger)
        {
            _jwtSettings = jwtSettings.Value;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<Result<AuthRefreshTokenResponseDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                var refreshToken = _httpContextAccessor.HttpContext?.Request.Cookies["refreshToken"];

                if (string.IsNullOrEmpty(refreshToken))
                    return Result<AuthRefreshTokenResponseDto>.Failure(Error.ValidationFailed, null, ErrorType.UnauthorizedError);
                var existedRefreshToken = await _unitOfWork.RefreshTokenRepository.GetEntity(s => s.Token == refreshToken && s.IsActive&&!s.IsDeleted, includes: new Func<IQueryable<RefreshToken>, IQueryable<RefreshToken>>[]
            {
                query => query.Include(s=>s.AppUser)
            });
                if (existedRefreshToken == null)
                    return Result<AuthRefreshTokenResponseDto>.Failure(Error.Custom("token", "refresh token doesnt exist"), null, ErrorType.NotFoundError);
                var user = existedRefreshToken.AppUser;
                if (user == null)
                    return Result<AuthRefreshTokenResponseDto>.Failure(Error.Unauthorized, null, ErrorType.UnauthorizedError);
                IList<string> roles = await _userManager.GetRolesAsync(user);
                var Audience = _jwtSettings.Audience;
                var SecretKey = _jwtSettings.secretKey;
                var Issuer = _jwtSettings.Issuer;
                var newrefreshTokenGenerated = _tokenService.GenerateRefreshToken();
                var newAccessToken = _tokenService.GetToken(SecretKey, Audience, Issuer, user, roles);
                existedRefreshToken.Token = newrefreshTokenGenerated;
                existedRefreshToken.Expires = DateTime.UtcNow.AddDays(7);
                await existedRefreshToken.UpdateStatus();
                await _unitOfWork.RefreshTokenRepository.Update(existedRefreshToken);
                await _unitOfWork.CommitTransactionAsync(cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                _httpContextAccessor.HttpContext?.Response.Cookies.Delete("refreshToken");
                _httpContextAccessor.HttpContext?.Response.Cookies.Append("refreshToken", newrefreshTokenGenerated, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Lax,
                    Expires = DateTime.UtcNow.AddDays(7)
                });
                return Result<AuthRefreshTokenResponseDto>.Success(new AuthRefreshTokenResponseDto { AccessToken = newAccessToken },null);

            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                _logger.LogError(ex, "Error occurred during user registration");
                return Result<AuthRefreshTokenResponseDto>.Failure(Error.InternalServerError, null, ErrorType.SystemError);

            }
        }
    }
}
