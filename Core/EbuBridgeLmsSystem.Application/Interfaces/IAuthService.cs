using EbuBridgeLmsSystem.Application.Dtos.Auth;
using EbuBridgeLmsSystem.Application.Dtos.Student;
using LearningManagementSystem.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Interfaces
{
    public interface IAuthService
    {
        Task<Result<UserGetDto>> RegisterForStudent(StudentRegistrationDto studentRegistrationDto);
        Task<Result<string>> SendVerificationCode(SendVerificationCodeDto sendVerificationCodeDto);
    }
}
