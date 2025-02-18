using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Features.AppUserFeature.Commands.SendVerificationCode
{
    public record SendVerificationCodeCommand:IRequest<Result<Unit>>
    {
        public string Email { get; init; }
    }
}
