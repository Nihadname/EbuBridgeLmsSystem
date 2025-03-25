using EbuBridgeLmsSystem.Application.Features.CourseFeature.Queries.GetAllCourse.GetAllCourseQuery;
using EbuBridgeLmsSystem.Application.Features.CourseFeature.Queries.GetByIdCourse;
using EbuBridgeLmsSystem.Domain.DomainDtos.Course;
using EbuBridgeLmsSystem.Domain.Entities;
using EbuBridgeLmsSystem.Domain.Repositories;
using LearningManagementSystem.DataAccess.Data.Implementations;
using Microsoft.EntityFrameworkCore;
using LanguageInCourseListItemDto = EbuBridgeLmsSystem.Domain.DomainDtos.Course.LanguageInCourseListItemDto;
using LessonInCourseReturnDto = EbuBridgeLmsSystem.Domain.DomainDtos.Course.LessonInCourseReturnDto;

namespace EbuBridgeLmsSystem.Persistance.Data.Implementations
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        private readonly ApplicationDbContext _context;
        public CourseRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<CourseDomainReturnDto> GetCourseReturnDtoByIdAsync(Guid Id,CancellationToken cancellationToken)
        {
            var course = await _context.Courses.Where(s => s.Id == Id && !s.IsDeleted).Include(p => p.Language)
        .Include(p => p.lessons).Select(existedCourse => new CourseDomainReturnDto() {
            Id = existedCourse.Id,
            Name = existedCourse.Name,
            Description = existedCourse.Description,
            difficultyLevel = existedCourse.difficultyLevel,
            DurationInHours = existedCourse.DurationInHours,
            EndDate = existedCourse.EndDate,
            StartDate = existedCourse.StartDate,
            ImageUrl = existedCourse.ImageUrl,
            Language = new LanguageInCourseListItemDto()
            {
                Id = existedCourse.Language.Id,
                Name = existedCourse.Language.Name,
            },
            Price = existedCourse.Price,
            Requirements = existedCourse.Requirements,
            lessonInCourses = existedCourse.lessons.Select(p => new LessonInCourseReturnDto()
            {
                Title = p.Title,
                EndTime = p.EndTime,
                Description = p.Description,
                Duration = p.Duration,
                ScheduledDate = p.ScheduledDate,
                GradingPolicy = p.GradingPolicy,
                LessonType = p.LessonType,
                MeetingLink = p.MeetingLink,
                StartTime = p.StartTime,
                Status = p.Status,
            }).ToList()
        }).FirstOrDefaultAsync(cancellationToken);
           return course;
        }
    }
}
