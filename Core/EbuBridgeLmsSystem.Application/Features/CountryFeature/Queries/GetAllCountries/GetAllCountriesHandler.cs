using AutoMapper;
using EbuBridgeLmsSystem.Application.Features.CountryFeature.CommanCommands;
using EbuBridgeLmsSystem.Domain.Entities;
using EbuBridgeLmsSystem.Domain.Entities.Common;
using EbuBridgeLmsSystem.Domain.Repositories;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EbuBridgeLmsSystem.Application.Features.CountryFeature.Queries.GetAllCountries
{
    public class GetAllCountriesHandler : IRequestHandler<GetAllCountriesQuery, Result<PaginatedResult<CountryListItemCommand>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllCountriesHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<PaginatedResult<CountryListItemCommand>>> Handle(GetAllCountriesQuery request, CancellationToken cancellationToken)
        {
            var countryQuery=await _unitOfWork.CountryRepository.GetQuery(null,true,true, includes: new Func<IQueryable<Country>, IQueryable<Country>>[] {
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
            var mappedResult = new PaginatedResult<CountryListItemCommand>
            {
                Data = paginationResult.Data.Select(country => new CountryListItemCommand
                {
                    Id = country.Id,
                    Name = country.Name,
                    IsDeleted = country.IsDeleted,
                    citiesinCountryListItemCommands=_mapper.Map<List<CitiesinCountryListItemCommand>>(country.Cities),
                }).ToList(),
                NextCursor = paginationResult.NextCursor
            };
            return Result<PaginatedResult<CountryListItemCommand>>.Success(mappedResult);
            
        }
    }
}
