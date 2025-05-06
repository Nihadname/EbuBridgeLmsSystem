using EbuBridgeLmsSystem.Domain.Entities.LmsSystem;

namespace EbuBridgeLmsSystem.Application.Dtos.Course
{
    public sealed record CourseCreateOrUpdateReturnDto
    {
        public Guid Id { get; init; }
        public string ImageUrl { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public DifficultyLevel difficultyLevel { get; set; }
        public TimeSpan Duration { get; set; }
        public Language Language { get; set; }
        public string Requirements { get; set; }
        public decimal Price { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
