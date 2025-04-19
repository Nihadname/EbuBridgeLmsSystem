using EbuBridgeLmsSystem.Application.Features.CourseFeature.Queries.GetAllCourse.GetAllCourseQuery;
using EbuBridgeLmsSystem.Domain.Entities;
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
            var existedCourse=await _unitOfWork.CourseRepository.GetCourseReturnDtoByIdAsync(request.Id,cancellationToken);
            if(existedCourse==null)
                return Result<CourseReturnDto>.Failure(Error.NotFound,null,ErrorType.NotFoundError);
            var mappedResult=new CourseReturnDto()
            {
                Id = existedCourse.Id,
                Name=existedCourse.Name,
                Description=existedCourse.Description,
                difficultyLevel=existedCourse.difficultyLevel,
                DurationInHours=existedCourse.DurationInHours,
                ImageUrl=existedCourse.ImageUrl,
                Language=new LanguageInCourseListItemDto()
                {
                    Id=existedCourse.Language.Id,
                    Name=existedCourse.Language.Name,
                },
                Price=existedCourse.Price,
                Requirements=existedCourse.Requirements,
                lessonInCourses=existedCourse.lessonInCourses.Select(p=>new LessonInCourseReturnDto() {
                    Title=p.Title,
                    EndTime=p.EndTime,
                    Description=p.Description,
                    Duration=p.Duration,
                    ScheduledDate=p.ScheduledDate,
                    GradingPolicy=p.GradingPolicy,
                    LessonType=p.LessonType,
                    MeetingLink=p.MeetingLink,
                    StartTime=p.StartTime,
                    Status=p.Status,
                }).ToList(),
                

            };
            return Result<CourseReturnDto>.Success(mappedResult, null);
        }
    }
}
