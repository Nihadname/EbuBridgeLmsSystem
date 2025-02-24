using EbuBridgeLmsSystem.Domain.Entities;
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

        public UserSoftDeleteHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result<Unit>> Handle(UserSoftDeleteCommand request, CancellationToken cancellationToken)
        {
            var existedUser=await _userManager.FindByIdAsync(request.Id);
            if (existedUser is null)
                return Result<Unit>.Failure("Error delete", "Error delete", null, ErrorType.NotFoundError);
            if(existedUser.IsDeleted is true)
                return Result<Unit>.Failure("Error delete", "already deleted", null, ErrorType.NotFoundError);
            existedUser.IsDeleted=true;
            existedUser.DeletedTime = DateTime.UtcNow;
            await _userManager.UpdateAsync(existedUser);
            return Result<Unit>.Success(Unit.Value);
        }
    }
}
