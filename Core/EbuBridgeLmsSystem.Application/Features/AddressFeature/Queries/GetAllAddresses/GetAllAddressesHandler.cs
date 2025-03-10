using AutoMapper;
using EbuBridgeLmsSystem.Domain.Entities;
using EbuBridgeLmsSystem.Domain.Entities.Common;
using EbuBridgeLmsSystem.Domain.Repositories;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace EbuBridgeLmsSystem.Application.Features.AddressFeature.Queries.GetAllAddresses
{
    public class GetAllAddressesHandler : IRequestHandler<GetAllAddressQuery, Result<PaginatedResult<AddressListItemQuery>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _cache;

        public GetAllAddressesHandler(IDistributedCache cache, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _cache = cache;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<PaginatedResult<AddressListItemQuery>>> Handle(GetAllAddressQuery request, CancellationToken cancellationToken)
        {
            string cacheKey = $"cities_{request.Cursor}_{request.Limit}_{request.searchQuery?.ToLower()}";
            var cachedData = await _cache.GetStringAsync(cacheKey);
            if (!string.IsNullOrWhiteSpace(cachedData))
            {
                var cachedResult = JsonSerializer.Deserialize<PaginatedResult<AddressListItemQuery>>(cachedData);
                return Result<PaginatedResult<AddressListItemQuery>>.Success(cachedResult);
            }
            var addressQuery = await _unitOfWork.AddressRepository.GetQuery(null, true, includes: new Func<IQueryable<Address>, IQueryable<Address>>[] {
                 query => query
            .Include(p => p.Country).Include(s=>s.City).Include(s=>s.AppUser) });
            if (!string.IsNullOrWhiteSpace(request.searchQuery))
            {
                addressQuery = addressQuery.Where(s => s.Country.Name.ToLower().Contains(request.searchQuery.ToLower())
                ||s.City.Name.ToLower().Contains(request.searchQuery.ToLower())||
                s.Region.ToLower().Contains(request.searchQuery.ToLower())||
                s.Street.ToLower().Contains(request.searchQuery.ToLower()));
            }
            addressQuery = addressQuery.OrderByDescending(s => s.CreatedTime);
            var paginationResult = await _unitOfWork.AddressRepository.GetPaginatedResultAsync(request.Cursor, request.Limit, includes: new Func<IQueryable<Address>, IQueryable<Address>>[] {
                 query => query
            .Include(p => p.Country).Include(s=>s.City).Include(s=>s.AppUser) });
            var mappedResult = new PaginatedResult<AddressListItemQuery>
            {
                Data = paginationResult.Data.Select(Address => new AddressListItemQuery
                {
                    Id = Address.Id,
                    Country = Address.Country.Name,
                    City = Address.City.Name,
                    Region = Address.Region,
                    Street = Address.Street,
                    AppUserInAdress = _mapper.Map<AppUserInAdress>(Address.AppUser)
                }).ToList(),
                NextCursor = paginationResult.NextCursor
            };
            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
            };
            await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(mappedResult), cacheOptions);
            return Result<PaginatedResult<AddressListItemQuery>>.Success(mappedResult);
        }
    }
}
