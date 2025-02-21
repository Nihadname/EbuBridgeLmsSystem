using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Features.AppUserFeature.Commands.ForgotPassword
{
    public record ForgotPasswordCommand
    {
        public string Email { get; init; }
        public string Token { get; init; }
    }
}
