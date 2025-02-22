using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Dtos.Auth
{
    public record AuthGetEmailDto
    {
        public string Email { get; init; }
       
    }
}
