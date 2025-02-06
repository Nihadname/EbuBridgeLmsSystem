using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Dtos.Auth
{
    public sealed class ResetPasswordTokenAndEmailDto
    {
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
