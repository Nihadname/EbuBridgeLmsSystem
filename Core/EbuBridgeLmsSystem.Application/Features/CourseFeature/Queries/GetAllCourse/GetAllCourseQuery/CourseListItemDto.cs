using EbuBridgeLmsSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Features.CourseFeature.Queries.GetAllCourse.GetAllCourseQuery
{
    public class CourseListItemDto
    {
        public Guid Id { get; init; }
        public string ImageUrl { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public DifficultyLevel difficultyLevel { get; init; }
        public ICollection<Lesson> lessons { get; init; }
        public int DurationInHours { get; init; }
        public string Requirements { get; init; }
        public decimal Price { get; init; }
        public DateTime CreatedTime { get; init; }
        public LanguageInCourseListItemDto Language { get; init; }
    }
    public sealed record LanguageInCourseListItemDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
    }
}
