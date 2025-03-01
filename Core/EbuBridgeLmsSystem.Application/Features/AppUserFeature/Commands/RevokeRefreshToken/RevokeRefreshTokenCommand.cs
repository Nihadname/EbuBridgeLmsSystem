using LearningManagementSystem.Core.Entities.Common;
using MediatR;

namespace EbuBridgeLmsSystem.Application.Features.AppUserFeature.Commands.RevokeRefreshToken
{
    public sealed record RevokeRefreshTokenCommand:IRequest<Result<Unit>>
    {
    }
}
