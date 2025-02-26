using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Dtos.Auth
{
    public sealed class ResetPasswordEmailDto
    {
        public string Email { get; set; }
        [JsonIgnore]
        public string Token { get; set; }
    }
}
