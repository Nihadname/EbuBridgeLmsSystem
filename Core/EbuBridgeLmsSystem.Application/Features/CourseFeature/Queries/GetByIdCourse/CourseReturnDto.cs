using EbuBridgeLmsSystem.Application.Features.CourseFeature.Queries.GetAllCourse.GetAllCourseQuery;
using EbuBridgeLmsSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Features.CourseFeature.Queries.GetByIdCourse
{
    public sealed record CourseReturnDto
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
    public sealed record LessonInCourseReturnDto
    {
        public string Title { get; set; }
        public DateTime ScheduledDate { get; set; }
        public TimeSpan Duration { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public LessonStatus Status { get; set; }
        public string Description { get; set; }
        public LessonType LessonType { get; set; }
        public string GradingPolicy { get; set; }
        public string MeetingLink { get; set; }
    }


}
