using EbuBridgeLmsSystem.Application.Dtos.Auth;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;

namespace EbuBridgeLmsSystem.Application.Features.AppUserFeature.Commands.CreateAppUserAsSaasStudent
{
    public sealed record CreateAppUserAsSaasStudentCommand : IRequest<Result<Unit>>
    {
        public required RegisterDto RegisterDto { get; init; }

    }
}
