using EbuBridgeLmsSystem.Domain.Entities.LmsSystem;

namespace EbuBridgeLmsSystem.Application.Dtos.Lesson
{
    public sealed class LessonCreateDto
    {
        public string Title { get; init; }
        public LessonStatus Status { get; init; }
        public string Description { get; init; }
        public LessonType LessonType { get; init; }
        public string GradingPolicy { get; init; }
        public Guid CourseId { get; init; }
    }
}
