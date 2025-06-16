using EbuBridgeLmsSystem.Domain.Entities.LmsSystem;
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
        public ICollection<LessonInCourseListItemDto> lessons { get; init; }
        public int DurationInHours { get; init; }
        public string Requirements { get; init; }
        public decimal Price { get; init; }
        public DateTime CreatedTime { get; init; }
        // public LanguageInCourseListItemDto Language { get; init; }
    }
    // public sealed record LanguageInCourseListItemDto
    // {
    //     public Guid Id { get; init; }
    //     public string Name { get; init; }
    // }
    public sealed record LessonInCourseListItemDto {
    public Guid Id { get; init; }
    public string Title { get; init; }
        public LessonStatus Status { get; set; }
        public string Description { get; set; }
        public LessonType LessonType { get; set; }
        public string GradingPolicy { get; set; }
    }

}
