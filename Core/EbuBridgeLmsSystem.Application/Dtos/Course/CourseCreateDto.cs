using EbuBridgeLmsSystem.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace EbuBridgeLmsSystem.Application.Dtos.Course
{
    public sealed record CourseCreateDto
    {
        public IFormFile? formFile { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public DifficultyLevel difficultyLevel { get; init; }
        public int DurationHours { get; init; }
        public Guid LanguageId { get; init; }
        public string Requirements { get; init; }
        public decimal Price { get; init; }


    }
  
}
