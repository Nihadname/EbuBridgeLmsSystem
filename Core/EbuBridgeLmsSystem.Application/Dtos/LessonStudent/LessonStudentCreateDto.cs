namespace EbuBridgeLmsSystem.Application.Dtos.LessonStudent
{
    public sealed record LessonStudentCreateDto
    {
        public Guid LessonId { get; init; }
        public Guid StudentId { get; init; }
    }
}
