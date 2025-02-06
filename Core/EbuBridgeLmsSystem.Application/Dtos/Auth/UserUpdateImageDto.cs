using Microsoft.AspNetCore.Http;

namespace EbuBridgeLmsSystem.Application.Dtos.Auth
{
    public sealed record UserUpdateImageDto
    {
        public IFormFile Image { get; init; }
    }
}
