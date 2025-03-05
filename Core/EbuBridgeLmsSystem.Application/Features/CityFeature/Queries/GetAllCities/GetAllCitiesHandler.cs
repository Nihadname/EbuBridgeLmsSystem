using AutoMapper;
using EbuBridgeLmsSystem.Application.Features.CountryFeature.CommanCommands;
using EbuBridgeLmsSystem.Domain.Entities;
using EbuBridgeLmsSystem.Domain.Entities.Common;
using EbuBridgeLmsSystem.Domain.Repositories;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Features.CityFeature.Queries.GetAllCities
{
    public class GetAllCitiesHandler : IRequestHandler<GetAllCitiesQuery, Result<PaginatedResult<CityListItemQuery>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllCitiesHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<PaginatedResult<CityListItemQuery>>> Handle(GetAllCitiesQuery request, CancellationToken cancellationToken)
        {
            var cityQuery = await _unitOfWork.CityRepository.GetQuery(null, true, true, includes: new Func<IQueryable<City>, IQueryable<City>>[] {
                 query => query
            .Include(p => p.Country) }
           );
            if (!string.IsNullOrWhiteSpace(request.searchQuery))
            {
                cityQuery = cityQuery.Where(s => s.Name.ToLower().Contains(request.searchQuery));
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
                }).ToList(),
                NextCursor = paginationResult.NextCursor
            };
            return Result<PaginatedResult<CityListItemQuery>>.Success(mappedResult);
        }
    }
}
