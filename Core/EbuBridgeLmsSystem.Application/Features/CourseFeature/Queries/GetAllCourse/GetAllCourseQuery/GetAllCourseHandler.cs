using EbuBridgeLmsSystem.Domain.Entities.Common;
using EbuBridgeLmsSystem.Domain.Repositories;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace EbuBridgeLmsSystem.Application.Features.CourseFeature.Queries.GetAllCourse.GetAllCourseQuery
{
    public class GetAllCourseHandler : IRequestHandler<GetAllCourseRequestQuery, Result<PaginatedResult<CourseListItemDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDistributedCache _cache;
        public GetAllCourseHandler(IUnitOfWork unitOfWork, IDistributedCache cache)
        {
            _unitOfWork = unitOfWork;
            _cache = cache;
        }

        public async Task<Result<PaginatedResult<CourseListItemDto>>> Handle(GetAllCourseRequestQuery request, CancellationToken cancellationToken)
        {
            var cacheKey = $"courses_{request.Cursor}_{request.Limit}_{request.SearchQuery?.ToLower()}";
            var cacheData =await _cache.GetStringAsync(cacheKey,cancellationToken);
            if (!string.IsNullOrWhiteSpace(cacheData))
            {
                var cachedResult = JsonSerializer.Deserialize<PaginatedResult<CourseListItemDto>>(cacheData);
                if (cachedResult != null)
                {
                    return Result<PaginatedResult<CourseListItemDto>>.Success(cachedResult,null);
                }
            }
            var courseQuery =  _unitOfWork.CourseRepository.GetSelected(course => new CourseListItemDto()
            {
                Id = course.Id,
                Name = course.Name,
                Description = course.Description,
                difficultyLevel = course.DifficultyLevel,
                DurationInHours = course.DurationInHours,
                ImageUrl = course.ImageUrl,
                Price = course.Price,
                Requirements = course.Requirements,
                CreatedTime= (DateTime)course.CreatedTime,
                lessons = course.Lessons.Select(s=>new LessonInCourseListItemDto()
                {
                    Id = s.Id,
                    Description = s.Description,
                    GradingPolicy = s.GradingPolicy,
                    LessonType = s.LessonType,
                    Status = s.Status,
                    Title = s.Title,
                }).Take(2).ToList(),
                Language=new LanguageInCourseListItemDto()
                {
                    Id=course.LanguageId,
                    Name=course.Name,
                }
            } );
            if (!string.IsNullOrWhiteSpace(request.SearchQuery))
            {
                courseQuery = courseQuery.Where(s => s.Name.ToLower().Contains(request.SearchQuery));
            }
            courseQuery = courseQuery.OrderByDescending(s => s.CreatedTime);
            var paginationResult = await _unitOfWork.CourseRepository.GetPaginatedResultAsync<CourseListItemDto,Guid>(query: courseQuery,
    cursor: request.Cursor,
    limit: request.Limit,
    sortKey: s => s.Id);
            var mappedResult = new PaginatedResult<CourseListItemDto>
            {
                Data = paginationResult.Data,
                NextCursor = paginationResult.NextCursor
            };
            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
            };
            await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(mappedResult), cacheOptions);
            return Result<PaginatedResult<CourseListItemDto>>.Success(mappedResult, null);


        }
    }
}
