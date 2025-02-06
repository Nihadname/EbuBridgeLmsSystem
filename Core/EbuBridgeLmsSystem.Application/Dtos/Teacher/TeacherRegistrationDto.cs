using EbuBridgeLmsSystem.Application.Dtos.Auth;

namespace EbuBridgeLmsSystem.Application.Dtos.Teacher
{
    public sealed record TeacherRegistrationDto
    {
        public RegisterDto Register { get; set; }
        public TeacherCreateDto Teacher { get; set; }
    }
}
