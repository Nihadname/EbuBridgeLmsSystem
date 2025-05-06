using EbuBridgeLmsSystem.Application.Interfaces;
using EbuBridgeLmsSystem.Domain.Entities.Common;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Features.AppUserFeature.Commands.UserSoftDelete
{
    public class UserSoftDeleteHandler : IRequestHandler<UserSoftDeleteCommand, Result<Unit>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IAppUserResolver _userResolver;
        public UserSoftDeleteHandler(UserManager<AppUser> userManager, IAppUserResolver userResolver)
        {
            _userManager = userManager;
            _userResolver = userResolver;
        }

        public async Task<Result<Unit>> Handle(UserSoftDeleteCommand request, CancellationToken cancellationToken)
        {
            var currentUser = await _userResolver.GetCurrentUserAsync();
            if (currentUser is null)
                return Result<Unit>.Failure(Error.Unauthorized, null, ErrorType.NotFoundError);
            if(currentUser.IsDeleted is true)
                return Result<Unit>.Failure(Error.Custom("Error delete", "already deleted"), null, ErrorType.NotFoundError);
            currentUser.IsDeleted=true;
            currentUser.DeletedTime = DateTime.UtcNow;
            var updateResult= await _userManager.UpdateAsync(currentUser);
            if (!updateResult.Succeeded)
            {
                return Result<Unit>.Failure(
                    Error.SystemError,
                    null,
                    ErrorType.SystemError);
            }
            return Result<Unit>.Success(Unit.Value,SuccessReturnType.NoContent);
        }
    }
}
