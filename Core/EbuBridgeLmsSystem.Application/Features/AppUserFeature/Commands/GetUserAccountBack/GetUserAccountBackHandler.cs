using EbuBridgeLmsSystem.Application.Interfaces;
using EbuBridgeLmsSystem.Domain.Entities;
using EbuBridgeLmsSystem.Domain.Entities.Common;
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
                return Result<Unit>.Failure(Error.Unauthorized, null, ErrorType.NotFoundError);
            if (currentUser.IsDeleted is false)
                return Result<Unit>.Failure(Error.Custom("Error delete", "account is active"), null, ErrorType.BusinessLogicError);
            var deletedDate = currentUser.DeletedTime;
            var today = DateTime.Now;
            var diffOfDates = today.Subtract((DateTime)deletedDate);
            if (diffOfDates.TotalDays >= 7)
            {
                return Result<Unit>.Failure(Error.Custom("Error delete", "you lost the chance of getting your account back becuase 7 days already passed"), null, ErrorType.BusinessLogicError);
            }
            currentUser.IsDeleted= false;
            currentUser.DeletedTime = null;
            var updateResult = await _userManager.UpdateAsync(currentUser);
            if (!updateResult.Succeeded)
            {
                return Result<Unit>.Failure(
                    Error.SystemError,
                    null,
                    ErrorType.SystemError);
            }
            return Result<Unit>.Success(Unit.Value);
        }
    }
}
