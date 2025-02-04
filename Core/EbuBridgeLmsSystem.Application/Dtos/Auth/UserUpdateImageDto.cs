using Microsoft.AspNetCore.Http;

namespace EbuBridgeLmsSystem.Application.Dtos.Auth
{
    public record UserUpdateImageDto
    {
        public IFormFile Image { get; init; }
    }
}
