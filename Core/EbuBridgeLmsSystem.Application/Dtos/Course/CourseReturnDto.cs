using EbuBridgeLmsSystem.Domain.Entities;

namespace EbuBridgeLmsSystem.Application.Dtos.Course
{
    public record CourseReturnDto
    {
        public Guid Id { get; init; }
        public string ImageUrl { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public ICollection<LessonInCourseReturnDto> Lessons { get; init; }
    }
    public record LessonInCourseReturnDto
    {
        public Guid Id { get; init; }
        public string Title { get; init; }
        public DateTime ScheduledDate { get; init; }
        public TimeSpan Duration { get; init; }
        public TimeSpan StartTime { get; init; }
        public TimeSpan EndTime { get; init; }
        public LessonStatus Status { get; init; }
        public string Description { get; init; }
        public LessonType LessonType { get; init; }
    }


}
