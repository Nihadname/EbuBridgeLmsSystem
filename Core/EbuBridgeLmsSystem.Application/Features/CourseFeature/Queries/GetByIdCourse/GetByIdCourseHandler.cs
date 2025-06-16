using EbuBridgeLmsSystem.Application.Features.CourseFeature.Queries.GetAllCourse.GetAllCourseQuery;
using EbuBridgeLmsSystem.Domain.Entities.Common;
using EbuBridgeLmsSystem.Domain.Repositories;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EbuBridgeLmsSystem.Application.Features.CourseFeature.Queries.GetByIdCourse
{
    public sealed class GetByIdCourseHandler : IRequestHandler<GetByIdCourseQuery, Result<CourseReturnDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetByIdCourseHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<CourseReturnDto>> Handle(GetByIdCourseQuery request, CancellationToken cancellationToken)
        {
            var existedCourse=await _unitOfWork.CourseRepository.GetSelected(existedCourse => new CourseReturnDto()
            {
                Id = existedCourse.Id,
                Name = existedCourse.Name,
                Description = existedCourse.Description,
                difficultyLevel = existedCourse.DifficultyLevel,
                DurationInHours = existedCourse.DurationInHours,
                ImageUrl = existedCourse.ImageUrl,
                Price = existedCourse.Price,
                Requirements = existedCourse.Requirements,
               
                lessonInCourses = existedCourse.Lessons.Select(p => new LessonInCourseReturnDto()
                {
                    Title = p.Title,
                    Description = p.Description,
                    GradingPolicy = p.GradingPolicy,
                    LessonType = p.LessonType,
                    Status = p.Status,
                }).Take(5).ToList()

            }).FirstOrDefaultAsync(s=>s.Id==request.Id,cancellationToken);
            if(existedCourse==null)
                return Result<CourseReturnDto>.Failure(Error.NotFound,null,ErrorType.NotFoundError);

            return Result<CourseReturnDto>.Success(existedCourse, null);
        }
    }
}
