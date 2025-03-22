using EbuBridgeLmsSystem.Application.Features.AuditLogFeature.Query.GetAllLogs;
using EbuBridgeLmsSystem.Application.Features.CityFeature.Queries.GetAllCities;
using EbuBridgeLmsSystem.Application.Features.CountryFeature.CommanCommands;
using EbuBridgeLmsSystem.Domain.Entities;
using EbuBridgeLmsSystem.Domain.Entities.Common;
using EbuBridgeLmsSystem.Domain.Repositories;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Features.CourseFeature.Queries.GetAllCourse.GetAllCourseQuery
{
    public class GetAllCourseHandler : IRequestHandler<GetAllCourseRequestQuery, Result<PaginatedResult<CourseListItemDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDistributedCache _cache;
        private readonly ILogger<GetAllCourseHandler> _logger;
        public GetAllCourseHandler(IUnitOfWork unitOfWork, IDistributedCache cache, ILogger<GetAllCourseHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _cache = cache;
            _logger = logger;
        }

        public async Task<Result<PaginatedResult<CourseListItemDto>>> Handle(GetAllCourseRequestQuery request, CancellationToken cancellationToken)
        {
            var cacheKey = $"courses_{request.Cursor}_{request.Limit}_{request.SearchQuery?.ToLower()}";
            var cacheData =await _cache.GetStringAsync(cacheKey,cancellationToken);
            if (!string.IsNullOrWhiteSpace(cacheKey))
            {
                var cachedResult = JsonSerializer.Deserialize<PaginatedResult<CourseListItemDto>>(cacheData);
                return Result<PaginatedResult<CourseListItemDto>>.Success(cachedResult);
            }
            var countryQuery = await _unitOfWork.CourseRepository.GetQuery(s => !s.IsDeleted, true, true, includes: new Func<IQueryable<Course>, IQueryable<Course>>[] {
                 query => query
            .Include(p => p.Language) });
            if (!string.IsNullOrWhiteSpace(request.SearchQuery))
            {
                countryQuery = countryQuery.Where(s => s.Name.ToLower().Contains(request.SearchQuery));
            }
            countryQuery = countryQuery.OrderByDescending(s => s.CreatedTime);
            var paginationResult = await _unitOfWork.CourseRepository.GetPaginatedResultAsync(request.Cursor, request.Limit, includes: new Func<IQueryable<Course>, IQueryable<Course>>[] {
                 query => query
            .Include(p => p.Language) });
            var mappedResult = new PaginatedResult<CourseListItemDto>
            {
                Data = paginationResult.Data.Select(course => new CourseListItemDto
                {
                    Id = course.Id,
                    Name = course.Name,
                    Description=course.Description,
                    difficultyLevel=course.difficultyLevel,
                    DurationInHours=course.DurationInHours,
                    StartDate = course.StartDate,
                    EndDate = course.EndDate,   
                    ImageUrl = course.ImageUrl,
                    Price = course.Price,
                    Requirements = course.Requirements,
                    Language=new LanguageInCourseListItemDto
                    {
                        Id=course.Language.Id,
                        Name = course.Language.Name,
                    }
                    

                }).AsEnumerable(),
                NextCursor = paginationResult.NextCursor
            };
            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
            };
            await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(mappedResult), cacheOptions);
            return Result<PaginatedResult<CourseListItemDto>>.Success(mappedResult);


        }
    }
}
