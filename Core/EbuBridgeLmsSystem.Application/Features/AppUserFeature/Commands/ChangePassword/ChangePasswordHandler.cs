using EbuBridgeLmsSystem.Application.Dtos.Auth;
using EbuBridgeLmsSystem.Application.Exceptions;
using EbuBridgeLmsSystem.Application.Interfaces;
using EbuBridgeLmsSystem.Domain.Entities;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Features.AppUserFeature.Commands.ChangePassword
{
    public class ChangePasswordHandler : IRequestHandler<ChangePasswordCommand, Result<Unit>>
    {
        private readonly IAppUserResolver _userResolver;
        private readonly UserManager<AppUser> _userManager;
        public ChangePasswordHandler(IAppUserResolver userResolver, UserManager<AppUser> userManager)
        {
            _userResolver = userResolver;
            _userManager = userManager;
        }

        public async Task<Result<Unit>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
           var currentUser= await _userResolver.GetCurrentUserAsync();
            if (currentUser is null)
            {
                return Result<Unit>.Failure("Id", "this user doesnt exist", null, ErrorType.UnauthorizedError);
            }
            var isTheSamePasswordWithTheCurrentOne = await _userManager.CheckPasswordAsync(currentUser, request.NewPassword);
            if (isTheSamePasswordWithTheCurrentOne)
            {
                return Result<Unit>.Failure("ChangePassword", "Password is the same with current one", null, ErrorType.BusinessLogicError);
            }
            var result = await _userManager.ChangePasswordAsync(currentUser, request.CurrentPassword, request.NewPassword);
            if (!result.Succeeded)
            {
                var errorMessages = result.Errors.ToDictionary(e => e.Code, e => e.Description);
                List<string> errors = new List<string>();
                foreach (KeyValuePair<string, string> keyValues in errorMessages)
                {
                    errors.Add(keyValues.Key + " " + keyValues.Value);
                }
                return Result<Unit>.Failure("ChangePassword", null, errors, ErrorType.SystemError);
            }
            return Result<Unit>.Success(Unit.Value);

        }
    }
}
