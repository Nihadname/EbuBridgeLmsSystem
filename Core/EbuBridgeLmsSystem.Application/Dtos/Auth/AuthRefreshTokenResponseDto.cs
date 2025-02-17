using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Dtos.Auth
{
    public sealed record AuthRefreshTokenResponseDto
    {
        public required  string RefreshToken { get; init; }
        public required string AccessToken { get; init; }

    }
}
