using AutoMapper;
using EbuBridgeLmsSystem.Domain.Entities;
using EbuBridgeLmsSystem.Domain.Entities.Common;
using EbuBridgeLmsSystem.Domain.Repositories;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace EbuBridgeLmsSystem.Application.Features.CityFeature.Queries.GetAllCities
{
    public class GetAllCitiesHandler : IRequestHandler<GetAllCitiesQuery, Result<PaginatedResult<CityListItemQuery>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _cache;
        public GetAllCitiesHandler(IUnitOfWork unitOfWork, IMapper mapper, IDistributedCache cache)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cache = cache;
        }
        public async Task<Result<PaginatedResult<CityListItemQuery>>> Handle(GetAllCitiesQuery request, CancellationToken cancellationToken)
        {
            string cacheKey = $"cities_{request.Cursor}_{request.Limit}_{request.searchQuery?.ToLower()}";
            var cachedData = await _cache.GetStringAsync(cacheKey);
            if (!string.IsNullOrWhiteSpace(cachedData))
            {
                var cachedResult = JsonSerializer.Deserialize<PaginatedResult<CityListItemQuery>>(cachedData);
                if (cachedResult != null)
                {
                    return Result<PaginatedResult<CityListItemQuery>>.Success(cachedResult, null);
                }
            }
            var cityQuery = await _unitOfWork.CityRepository.GetQuery(s=>!s.IsDeleted, true, includes: new Func<IQueryable<City>, IQueryable<City>>[] {
                 query => query
            .Include(p => p.Country) }
           );
            if (!string.IsNullOrWhiteSpace(request.searchQuery))
            {
                cityQuery = cityQuery.Where(s => s.Name.ToLower().Contains(request.searchQuery.ToLower()));
            }
            cityQuery = cityQuery.OrderByDescending(s => s.CreatedTime);
            var paginationResult = await _unitOfWork.CityRepository.GetPaginatedResultAsync(request.Cursor, request.Limit, includes: new Func<IQueryable<City>, IQueryable<City>>[] {
                 query => query
            .Include(p => p.Country) });
            var mappedResult = new PaginatedResult<CityListItemQuery>
            {
                Data = paginationResult.Data.Select(city => new CityListItemQuery
                {
                    Id = city.Id,
                    Name = city.Name,
                    countryInCityListItemQuery=_mapper.Map<CountryInCityListItemQuery>(city.Country)
                }).AsEnumerable(),
                NextCursor = paginationResult.NextCursor
            };
            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
            };
            await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(mappedResult), cacheOptions);
            return Result<PaginatedResult<CityListItemQuery>>.Success(mappedResult, null);
        }
    }
}
