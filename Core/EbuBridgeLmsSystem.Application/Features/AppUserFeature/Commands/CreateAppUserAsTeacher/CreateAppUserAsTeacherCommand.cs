using EbuBridgeLmsSystem.Application.Dtos.Auth;
using EbuBridgeLmsSystem.Application.Dtos.Teacher;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;

namespace EbuBridgeLmsSystem.Application.Features.AppUserFeature.Commands.CreateAppUserAsTeacher
{
    public record CreateAppUserAsTeacherCommand:IRequest<Result<UserGetDto>>
    {
        public required TeacherCreateDto TeacherCreateDto { get; init; }
        public required RegisterDto RegisterDto { get; init; }
    }
}
