namespace EbuBridgeLmsSystem.Application.Dtos.Course
{
    public record CourseUpdateDto
    {
        public string Name { get; init; }
        public string Description { get; init; }
    }
}
