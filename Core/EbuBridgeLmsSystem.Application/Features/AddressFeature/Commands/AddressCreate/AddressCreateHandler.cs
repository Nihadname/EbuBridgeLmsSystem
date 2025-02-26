using EbuBridgeLmsSystem.Application.Interfaces;
using EbuBridgeLmsSystem.Domain.Entities;
using EbuBridgeLmsSystem.Domain.Entities.Common;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Features.AddressFeature.Commands.AddressCreate
{
    public class AddressCreateHandler : IRequestHandler<AddressCreateCommand, Result<Unit>>
    {
        private readonly   IAppUserResolver _userResolver;
        private readonly UserManager<AppUser> _userManager;
        public AddressCreateHandler(IAppUserResolver userResolver, UserManager<AppUser> userManager)
        {
            _userResolver = userResolver;
            _userManager = userManager;
        }

        public async Task<Result<Unit>> Handle(AddressCreateCommand request, CancellationToken cancellationToken)
        {
            var currentUserInSystem = await _userResolver.GetCurrentUserAsync();
            if (currentUserInSystem == null)
                Result<Unit>.Failure(Error.Unauthorized,null, ErrorType.UnauthorizedError);
            return Result<Unit>.Success(Unit.Value);
        }
    }
}
