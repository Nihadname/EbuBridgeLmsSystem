using EbuBridgeLmsSystem.Application.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Dtos.Student
{
    public record StudentRegistrationDto
    {
        public StudentCreateDto  StudentCreateDto { get; init; }
        public RegisterDto RegisterDto { get; init; }
    }
}
