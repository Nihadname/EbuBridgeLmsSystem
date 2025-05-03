using EbuBridgeLmsSystem.Domain.Entities;
using EbuBridgeLmsSystem.Domain.Entities.Common;
using EbuBridgeLmsSystem.Domain.Repositories;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace EbuBridgeLmsSystem.Application.Features.AddressFeature.Queries.GetAllAddresses
{
    public class GetAllAddressesHandler : IRequestHandler<GetAllAddressQuery, Result<PaginatedResult<AddressListItemQuery>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDistributedCache _cache;

        public GetAllAddressesHandler(IDistributedCache cache, IUnitOfWork unitOfWork)
        {
            _cache = cache;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<PaginatedResult<AddressListItemQuery>>> Handle(GetAllAddressQuery request, CancellationToken cancellationToken)
        {
            string cacheKey = $"cities_{request.Cursor}_{request.Limit}_{request.searchQuery?.ToLower()}";
            var cachedData = await _cache.GetStringAsync(cacheKey);
            if (!string.IsNullOrWhiteSpace(cachedData))
            {
                var cachedResult = JsonSerializer.Deserialize<PaginatedResult<AddressListItemQuery>>(cachedData);
                return Result<PaginatedResult<AddressListItemQuery>>.Success(cachedResult, null);
            }
            var addressQuery =  _unitOfWork.AddressRepository.GetSelected(selector: Address => new AddressListItemQuery {
                Id = Address.Id,
                Country = Address.Country.Name,
                City = Address.City.Name,
                Region = Address.Region,
                Street = Address.Street,
               CreatedTime = (DateTime)Address.CreatedTime,
               AppUserInAdress=new AppUserInAdress() { 
                  Id=Address.AppUserId,
                  UserName=Address.AppUser.UserName               }
            });
            if (!string.IsNullOrWhiteSpace(request.searchQuery))
            {
                addressQuery = addressQuery.Where(s => s.Country.ToLower().Contains(request.searchQuery.ToLower())
                ||s.City.ToLower().Contains(request.searchQuery.ToLower())||
                s.Region.ToLower().Contains(request.searchQuery.ToLower())||
                s.Street.ToLower().Contains(request.searchQuery.ToLower()));
            }
            addressQuery = addressQuery.OrderByDescending(s => s.CreatedTime);
            var paginationResult = await _unitOfWork.AddressRepository.GetPaginatedResultAsync<AddressListItemQuery, Guid>(
    query: addressQuery,
    cursor: request.Cursor,
    limit: request.Limit,
    sortKey: s => s.Id);
            var mappedResult = new PaginatedResult<AddressListItemQuery>
            {
                Data = paginationResult.Data,
                NextCursor = paginationResult.NextCursor
            };
            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
            };
            await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(mappedResult), cacheOptions);
            return Result<PaginatedResult<AddressListItemQuery>>.Success(mappedResult,null);
        }
    }
}
