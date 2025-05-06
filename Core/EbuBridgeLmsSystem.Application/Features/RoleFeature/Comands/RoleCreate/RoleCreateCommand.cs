using LearningManagementSystem.Core.Entities.Common;
using MediatR;

namespace EbuBridgeLmsSystem.Application.Features.RoleFeature.Comands.RoleCreate
{
    public sealed record RoleCreateCommand:IRequest<Result<Unit>>
    {
        public string Name { get; init; }
    }
}
