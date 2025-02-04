namespace EbuBridgeLmsSystem.Application.Dtos.Course
{
    public record CourseListItemDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public string ImageUrl { get; init; }
    }
}
