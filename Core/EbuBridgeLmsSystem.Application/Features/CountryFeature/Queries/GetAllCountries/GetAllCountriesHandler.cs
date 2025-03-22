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
    public class GetAllCountriesHandler : IRequestHandler<GetAllCountriesQuery, Result<PaginatedResult<CountryListItemQuery>>>
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
            var cachedData = await _cache.GetStringAsync(cacheKey);
            if (!string.IsNullOrEmpty(cachedData))
            {
                var cachedResult = JsonSerializer.Deserialize<PaginatedResult<CountryListItemQuery>>(cachedData);
                return Result<PaginatedResult<CountryListItemQuery>>.Success(cachedResult);
            }
            var countryQuery =await _unitOfWork.CountryRepository.GetQuery(s=>!s.IsDeleted,true,true, includes: new Func<IQueryable<Country>, IQueryable<Country>>[] {
                 query => query
            .Include(p => p.Cities) }
           );
            if (!string.IsNullOrWhiteSpace(request.searchQuery))
            {
                countryQuery = countryQuery.Where(s => s.Name.ToLower().Contains(request.searchQuery));
            }
            countryQuery = countryQuery.OrderByDescending(s => s.CreatedTime);
            var paginationResult = await _unitOfWork.CountryRepository.GetPaginatedResultAsync(request.Cursor, request.Limit, includes: new Func<IQueryable<Country>, IQueryable<Country>>[] {
                 query => query
            .Include(p => p.Cities) });
            var mappedResult = new PaginatedResult<CountryListItemQuery>
            {
                Data = paginationResult.Data.Select(country => new CountryListItemQuery
                {
                    Id = country.Id,
                    Name = country.Name,
                    IsDeleted = country.IsDeleted,
                    citiesinCountryListItemCommands=_mapper.Map<List<CitiesinCountryListItemCommand>>(country.Cities),
                }).AsEnumerable(),
                NextCursor = paginationResult.NextCursor
            };
            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10) 
            };
            await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(mappedResult), cacheOptions);
            return Result<PaginatedResult<CountryListItemQuery>>.Success(mappedResult);
            
        }
    }
}
