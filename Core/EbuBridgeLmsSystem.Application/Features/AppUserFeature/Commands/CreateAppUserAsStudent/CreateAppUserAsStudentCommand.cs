using EbuBridgeLmsSystem.Application.Dtos.Auth;
using EbuBridgeLmsSystem.Application.Dtos.Student;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using System.Text.Json.Serialization;

namespace EbuBridgeLmsSystem.Application.Features.AppUserFeature.Commands.CreateAppUserAsStudent
{
    public record CreateAppUserAsStudentCommand:IRequest<Result<UserGetDto>>
    {
        public StudentCreateDto StudentCreateDto { get; init; }
        public RegisterDto RegisterDto { get; init; }
    }
}
