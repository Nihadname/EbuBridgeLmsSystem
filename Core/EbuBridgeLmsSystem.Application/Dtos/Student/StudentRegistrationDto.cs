using EbuBridgeLmsSystem.Application.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Dtos.Student
{
    public class StudentRegistrationDto
    {
        public StudentCreateDto  StudentCreateDto { get; set; }
        public RegisterDto RegisterDto { get; set; }
    }
}
