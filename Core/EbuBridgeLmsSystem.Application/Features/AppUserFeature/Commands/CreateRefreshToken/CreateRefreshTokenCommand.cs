using EbuBridgeLmsSystem.Application.Dtos.Auth;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;

namespace EbuBridgeLmsSystem.Application.Features.AppUserFeature.Commands.CreateRefreshToken
{
    public record CreateRefreshTokenCommand:IRequest<Result<AuthRefreshTokenResponseDto>>
    {
    }
}
