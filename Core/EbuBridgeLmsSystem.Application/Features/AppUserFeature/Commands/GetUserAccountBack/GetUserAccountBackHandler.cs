using EbuBridgeLmsSystem.Application.Interfaces;
using EbuBridgeLmsSystem.Domain.Entities;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EbuBridgeLmsSystem.Application.Features.AppUserFeature.Commands.GetUserAccountBack
{
    public class GetUserAccountBackHandler : IRequestHandler<GetUserAccountBackCommand, Result<Unit>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IAppUserResolver _userResolver;
        public GetUserAccountBackHandler(UserManager<AppUser> userManager, IAppUserResolver userResolver)
        {
            _userManager = userManager;
            _userResolver = userResolver;
        }

        public async Task<Result<Unit>> Handle(GetUserAccountBackCommand request, CancellationToken cancellationToken)
        {
            var currentUser = await _userResolver.GetCurrentUserAsync();
            if (currentUser is null)
                return Result<Unit>.Failure("Error delete", "user not found", null, ErrorType.NotFoundError);
            if (currentUser.IsDeleted is false)
                return Result<Unit>.Failure("Error delete", "account is active", null, ErrorType.NotFoundError);
            var deletedDate = currentUser.DeletedTime;
            var today = DateTime.Now;
            var diffOfDates = today.Subtract((DateTime)deletedDate);
            if (diffOfDates.Days >= 7)
            {
                return Result<Unit>.Failure("Error delete", "you lost the chance of getting your account back becuase 7 days already passed", null, ErrorType.BusinessLogicError);
            }
            currentUser.IsDeleted= false;
            currentUser.DeletedTime = null;
            var updateResult = await _userManager.UpdateAsync(currentUser);
            if (!updateResult.Succeeded)
            {
                return Result<Unit>.Failure(
                    "Deletion Failed",
                    "Failed to update user status.",
                    null,
                    ErrorType.SystemError);
            }
            return Result<Unit>.Success(Unit.Value);
        }
    }
}
