using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Dtos.Auth
{
    public sealed record LoginDto
    {
        public string UserNameOrGmail { get; init; }
        public string Password { get; init; }
    }
}
