using EbuBridgeLmsSystem.Application.Dtos.Auth;
using EbuBridgeLmsSystem.Application.Interfaces;
using EbuBridgeLmsSystem.Application.Settings;
using EbuBridgeLmsSystem.Domain.Entities;
using EbuBridgeLmsSystem.Domain.Repositories;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace EbuBridgeLmsSystem.Application.Features.AppUserFeature.Commands.CreateRefreshToken
{
    public class CreateRefreshTokenHandler : IRequestHandler<CreateRefreshTokenCommand, Result<AuthRefreshTokenResponseDto>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly JwtSettings _jwtSettings;
        private readonly ITokenService _tokenService;


        public CreateRefreshTokenHandler(IOptions<JwtSettings> jwtSettings, UserManager<AppUser> userManager, IHttpContextAccessor contextAccessor, IUnitOfWork unitOfWork, ITokenService tokenService)
        {
            _userManager = userManager;
            _contextAccessor = contextAccessor;
            _unitOfWork = unitOfWork;
            _jwtSettings = jwtSettings.Value;
            _tokenService = tokenService;
        }

        public async Task<Result<AuthRefreshTokenResponseDto>> Handle(CreateRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var refreshToken = _contextAccessor.HttpContext?.Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(refreshToken))
                return  Result<AuthRefreshTokenResponseDto>.Failure("RefreshToken", "Refresh token is missing",null, ErrorType.UnauthorizedError);
            var existedrefreshToken=await _unitOfWork.RefreshTokenRepository.GetEntity(s=>s.Token==refreshToken, includes: new Func<IQueryable<RefreshToken>, IQueryable<RefreshToken>>[]
            {
                query => query.Include(s=>s.AppUser)
            });
            if (existedrefreshToken == null)
                return Result<AuthRefreshTokenResponseDto>.Failure("RefreshToken", "Invalid refresh token",null, ErrorType.UnauthorizedError);
            if (!existedrefreshToken.IsActive)
                return Result<AuthRefreshTokenResponseDto>.Failure("RefreshToken", "Invalid or expired refresh token",null, ErrorType.UnauthorizedError);
            var user = existedrefreshToken.AppUser;
            if (user == null)
                return Result<AuthRefreshTokenResponseDto>.Failure("Id", "User does not exist",null, ErrorType.UnauthorizedError);
            IList<string> roles = await _userManager.GetRolesAsync(user);
            var Audience = _jwtSettings.Audience;
            var SecretKey = _jwtSettings.secretKey;
            var Issuer = _jwtSettings.Issuer;
            var newrefreshTokenGenerated = _tokenService.GenerateRefreshToken();
            var newAccessToken = _tokenService.GetToken(SecretKey, Audience, Issuer, user, roles);
            RefreshToken refreshTokenAsObject = new RefreshToken { AppUserId = user.Id, Token = newrefreshTokenGenerated, Expires = DateTime.UtcNow.AddDays(7) };
            await refreshTokenAsObject.UpdateStatus();
            await _unitOfWork.RefreshTokenRepository.Create(refreshTokenAsObject);
            await _unitOfWork.SaveChangesAsync();
            _contextAccessor.HttpContext?.Response.Cookies.Append("refreshToken", newrefreshTokenGenerated, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(7)
            });
            return Result<AuthRefreshTokenResponseDto>.Success(new AuthRefreshTokenResponseDto { AccessToken = newAccessToken, RefreshToken = refreshTokenAsObject.Token });
        }
    }
}
