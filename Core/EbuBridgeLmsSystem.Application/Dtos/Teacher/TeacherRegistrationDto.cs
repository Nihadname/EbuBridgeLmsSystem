using EbuBridgeLmsSystem.Application.Dtos.Auth;

namespace EbuBridgeLmsSystem.Application.Dtos.Teacher
{
    public sealed record TeacherRegistrationDto
    {
        public required RegisterDto RegisterDto { get; init; }
        public required TeacherCreateDto TeacherCreateDto { get; init; }
    }
}
