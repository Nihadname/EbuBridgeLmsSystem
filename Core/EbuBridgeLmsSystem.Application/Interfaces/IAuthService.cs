using EbuBridgeLmsSystem.Application.Dtos.Auth;
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
        Task<Result<UserGetDto>> RegisterForStudent(RegisterDto registerDto);
        Task<Result<string>> SendVerificationCode(SendVerificationCodeDto sendVerificationCodeDto);
    }
}
