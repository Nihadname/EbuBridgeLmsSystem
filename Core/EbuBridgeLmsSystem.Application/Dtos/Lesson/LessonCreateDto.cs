namespace EbuBridgeLmsSystem.Application.Dtos.Lesson
{
    public sealed class LessonCreateDto
    {
        public string Title { get; set; }
        public DateTime ScheduledDate { get; set; }
        public Guid CourseId { get; set; }
        public Guid TeacherId { get; set; }
    }
}
