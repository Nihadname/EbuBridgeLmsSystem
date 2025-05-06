using EbuBridgeLmsSystem.Application.Features.CourseFeature.Queries.GetAllCourse.GetAllCourseQuery;
using EbuBridgeLmsSystem.Domain.Entities.LmsSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Features.CourseFeature.Queries.GetByIdCourse
{
    public sealed record CourseReturnDto
    {
        public Guid Id { get; init; }
        public string ImageUrl { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public DifficultyLevel difficultyLevel { get; init; }
        public int DurationInHours { get; init; }
        public string Requirements { get; init; }
        public decimal Price { get; init; }
        public LanguageInCourseListItemDto Language { get; init; }
        public List<LessonInCourseReturnDto> lessonInCourses { get; init; }
    }
    public sealed record LessonInCourseReturnDto
    {
        public string Title { get; init; }
        public DateTime ScheduledDate { get; init; }
        public TimeSpan Duration { get; init; }
        public TimeSpan StartTime { get; init; }
        public TimeSpan EndTime { get; init; }
        public LessonStatus Status { get; init; }
        public string Description { get; init; }
        public LessonType LessonType { get; init; }
        public string GradingPolicy { get; init; }
        public string MeetingLink { get; init; }
    }


}
