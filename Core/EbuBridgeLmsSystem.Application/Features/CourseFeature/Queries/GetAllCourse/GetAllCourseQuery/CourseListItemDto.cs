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
        public Guid Id { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DifficultyLevel difficultyLevel { get; set; }
        public ICollection<Lesson> lessons { get; set; }
        public int DurationInHours { get; set; }
        public string Requirements { get; set; }
        public decimal Price { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public LanguageInCourseListItemDto Language { get; set; }
    }
    public sealed record LanguageInCourseListItemDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
    }
}
