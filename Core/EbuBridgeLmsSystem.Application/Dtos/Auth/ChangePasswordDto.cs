using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Dtos.Auth
{
    public sealed record ChangePasswordDto
    {
        public string CurrentPassword { get; init; }
        public string NewPassword { get; init; }
        public string ConfirmPassword { get; init; }
    }
}
