using EbuBridgeLmsSystem.Application.Dtos.Auth;

namespace EbuBridgeLmsSystem.Application.Dtos.Teacher
{
    public record TeacherRegistrationDto
    {
        public RegisterDto Register { get; set; }
        public TeacherCreateDto Teacher { get; set; }
    }
}
