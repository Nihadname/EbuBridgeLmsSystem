using EbuBridgeLmsSystem.Application.Dtos.Auth;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;

namespace EbuBridgeLmsSystem.Application.Features.AppUser.Commands.CreateAppUserAsStudent
{
    public class CreateAppUserAsStudentCommand:IRequest<Result<UserGetDto>>
    {
        public string FullName { get; init; }
        public string UserName { get; init; }
        public string Email { get; init; }
        public string PhoneNumber { get; init; }
        public string Password { get; init; }
        public string RepeatPassword { get; init; }
        public DateTime BirthDate { get; init; }
    }
}
