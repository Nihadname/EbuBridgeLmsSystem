using EbuBridgeLmsSystem.Application.Dtos.Auth;
using EbuBridgeLmsSystem.Application.Dtos.Parent;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;

namespace EbuBridgeLmsSystem.Application.Features.AppUserFeature.Commands.CreateAppUserAsParent
{
    public sealed  record CreateAppUserAsParentCommand:IRequest<Result<UserGetDto>>
    {
        public required ParentCreateDto ParentCreateDto { get; set; }
        public required RegisterDto RegisterDto { get; set; }
    }
}
