using EbuBridgeLmsSystem.Application.Interfaces;
using EbuBridgeLmsSystem.Domain.Entities.Common;
using EbuBridgeLmsSystem.Domain.Repositories;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Features.AppUserFeature.Commands.RevokeRefreshToken
{
    public class RevokeRefreshTokenHandler : IRequestHandler<RevokeRefreshTokenCommand, Result<Unit>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IAppUserResolver _appUserResolver;
        private readonly IUnitOfWork _unitOfWork;
        public RevokeRefreshTokenHandler(IHttpContextAccessor contextAccessor, UserManager<AppUser> userManager, IAppUserResolver appUserResolver, IUnitOfWork unitOfWork = null)
        {
            _contextAccessor = contextAccessor;
            _userManager = userManager;
            _appUserResolver = appUserResolver;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(RevokeRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var refreshToken = _contextAccessor.HttpContext?.Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(refreshToken))
                return Result<Unit>.Failure(Error.Custom("RefreshToken", "Refresh token is missing"), null, ErrorType.UnauthorizedError);
            var currentUser= await _appUserResolver.GetCurrentUserAsync();
            if (currentUser == null)
                return Result<Unit>.Failure(Error.Unauthorized,null, ErrorType.UnauthorizedError);
            var existedRefreshToken=await _unitOfWork.RefreshTokenRepository.GetEntity(s=>s.Token==refreshToken&&s.IsActive&&!s.IsDeleted);
            if (existedRefreshToken == null)
                return Result<Unit>.Failure(Error.NotFound, null, ErrorType.NotFoundError);
            if (existedRefreshToken.AppUser == null || existedRefreshToken.AppUser.Id != currentUser.Id)
                return Result<Unit>.Failure(Error.Custom("User", "User doesnt match or exists"),null, ErrorType.NotFoundError);
            existedRefreshToken.Revoked = DateTime.UtcNow;
            existedRefreshToken.Expires = DateTime.UtcNow;
            await existedRefreshToken.UpdateStatus();
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result<Unit>.Success(Unit.Value, null);
        }
    }
}
