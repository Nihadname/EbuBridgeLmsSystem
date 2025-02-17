using EbuBridgeLmsSystem.Application.Dtos.Auth;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;

namespace EbuBridgeLmsSystem.Application.Features.AppUserFeature.Commands.Login
{
    public record LoginCommand:IRequest<Result<AuthResponseDto>>
    {
        public string UserNameOrGmail { get; init; }
        public string Password { get; init; }
    }
}
