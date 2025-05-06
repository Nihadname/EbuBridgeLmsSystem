using EbuBridgeLmsSystem.Domain.Entities.Common;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EbuBridgeLmsSystem.Application.Features.RoleFeature.Comands.RoleCreate
{
    public sealed class RoleCreateHandler : IRequestHandler<RoleCreateCommand, Result<Unit>>
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleCreateHandler(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<Result<Unit>> Handle(RoleCreateCommand request, CancellationToken cancellationToken)
        {
            if (await _roleManager.RoleExistsAsync(request.Name))
                return Result<Unit>.Failure(Error.DuplicateConflict, null, ErrorType.BusinessLogicError);
            await _roleManager.CreateAsync(new IdentityRole()
            {
                Name = request.Name,
            });
            return Result<Unit>.Success(Unit.Value, SuccessReturnType.Created);

        }
    }
}
