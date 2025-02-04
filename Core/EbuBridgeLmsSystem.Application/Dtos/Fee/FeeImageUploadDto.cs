using Microsoft.AspNetCore.Http;

namespace EbuBridgeLmsSystem.Application.Dtos.Fee
{
    public record FeeImageUploadDto
    {
        public Guid Id { get; set; }
        public IFormFile image { get; init; }
    }
}
