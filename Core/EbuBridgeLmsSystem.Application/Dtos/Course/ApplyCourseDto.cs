using System.Text.Json.Serialization;

namespace EbuBridgeLmsSystem.Application.Dtos.Course
{
    public sealed record ApplyCourseDto
    {
        public Guid StudentId { get; init; }
        public Guid CourseId { get; init; }
    }
}
