namespace EbuBridgeLmsSystem.Application.Dtos.Course
{
    public sealed record CourseSelectItemDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
    }
}
