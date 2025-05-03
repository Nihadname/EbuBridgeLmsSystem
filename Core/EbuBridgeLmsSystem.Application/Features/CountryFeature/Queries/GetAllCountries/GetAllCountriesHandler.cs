using AutoMapper;
using EbuBridgeLmsSystem.Application.Features.CountryFeature.CommanCommands;
using EbuBridgeLmsSystem.Domain.Entities;
using EbuBridgeLmsSystem.Domain.Entities.Common;
using EbuBridgeLmsSystem.Domain.Repositories;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace EbuBridgeLmsSystem.Application.Features.CountryFeature.Queries.GetAllCountries
{
    public sealed class GetAllCountriesHandler : IRequestHandler<GetAllCountriesQuery, Result<PaginatedResult<CountryListItemQuery>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _cache;
        public GetAllCountriesHandler(IUnitOfWork unitOfWork, IMapper mapper, IDistributedCache cache)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<Result<PaginatedResult<CountryListItemQuery>>> Handle(GetAllCountriesQuery request, CancellationToken cancellationToken)
        {
            string cacheKey = $"countries_{request.Cursor}_{request.Limit}_{request.searchQuery?.ToLower()}";
            var cachedData = await _cache.GetStringAsync(cacheKey,cancellationToken);
            if (!string.IsNullOrEmpty(cachedData))
            {
                var cachedResult = JsonSerializer.Deserialize<PaginatedResult<CountryListItemQuery>>(cachedData);
                if (cachedResult != null)
                {
                    return Result<PaginatedResult<CountryListItemQuery>>.Success(cachedResult,null);
                }
            }
            var countryQuery = _unitOfWork.CountryRepository.GetSelected(s => new CountryListItemQuery()
            {
                Id = s.Id,
                Name = s.Name,
                IsDeleted = s.IsDeleted,
                CreatedTime= (DateTime)s.CreatedTime,
                citiesinCountryListItemCommands = s.Cities.Select(s => new CitiesinCountryListItemCommand()
                {
                    Id = s.Id,
                    Name = s.Name,
                }).ToList()
            });
            if (!string.IsNullOrWhiteSpace(request.searchQuery))
            {
                countryQuery = countryQuery.Where(s => s.Name.ToLower().Contains(request.searchQuery));
            }
            countryQuery = countryQuery.OrderByDescending(s => s.CreatedTime);
            var paginationResult = await _unitOfWork.CountryRepository.GetPaginatedResultAsync<CountryListItemQuery,Guid>(query: countryQuery,
    cursor: request.Cursor,
    limit: request.Limit,
    sortKey: s => s.Id);
            var mappedResult = new PaginatedResult<CountryListItemQuery>
            {
                Data = paginationResult.Data,
                NextCursor = paginationResult.NextCursor
            };
            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10) 
            };
            await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(mappedResult), cacheOptions);
            return Result<PaginatedResult<CountryListItemQuery>>.Success(mappedResult, null);
            
        }
    }
}
