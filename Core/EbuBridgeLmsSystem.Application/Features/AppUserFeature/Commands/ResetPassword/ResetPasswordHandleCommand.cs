using EbuBridgeLmsSystem.Application.Dtos.Auth;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Features.AppUserFeature.Commands.ResetPassword
{
    public record ResetPasswordHandleCommand:IRequest<Result<Unit>>
    {
        public ResetPasswordTokenAndEmailDto ResetPasswordTokenAndEmailDto { get; init; }
        public ResetPasswordDto resetPasswordDto { get; init; }
    }
}
