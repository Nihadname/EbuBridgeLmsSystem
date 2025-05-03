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
            var cityQuery =  _unitOfWork.CityRepository.GetSelected(s=> new CityListItemQuery(){
                Id = s.Id,
                Name = s.Name,
                countryInCityListItemQuery=new CountryInCityListItemQuery()
                {
                    Id = s.Id,
                    Name= s.Name,
                }
            }
           );
            if (!string.IsNullOrWhiteSpace(request.searchQuery))
            {
                cityQuery = cityQuery.Where(s => s.Name.ToLower().Contains(request.searchQuery.ToLower()));
            }
            cityQuery = cityQuery.OrderByDescending(s => s.CreatedTime);
            var paginationResult = await _unitOfWork.CityRepository.GetPaginatedResultAsync(query: cityQuery,
    cursor: request.Cursor,
    limit: request.Limit,
    sortKey: s => s.Id);
            var mappedResult = new PaginatedResult<CityListItemQuery>
            {
                Data = paginationResult.Data,
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
