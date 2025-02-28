using EbuBridgeLmsSystem.Application.Features.CountryFeature.Queries.CommanCommands;
using EbuBridgeLmsSystem.Domain.Entities;
using EbuBridgeLmsSystem.Domain.Entities.Common;
using EbuBridgeLmsSystem.Domain.Repositories;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Features.CountryFeature.Queries.GetAllCountries
{
    public class GetAllCountriesHandler : IRequestHandler<GetAllCountriesQuery, Result<PaginatedResult<CountryListItemCommand>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllCountriesHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
            var paginationResult = await _unitOfWork.CountryRepository.GetPaginatedResultAsync(request.Cursor, request.Limit);
            var mappedResult = new PaginatedResult<CountryListItemCommand>
            {
                Data = paginationResult.Data.Select(country => new CountryListItemCommand
                {
                    Id = country.Id,
                    Name = country.Name,
                    IsDeleted = country.IsDeleted,
                }).ToList(),
                NextCursor = paginationResult.NextCursor
            };
            return Result<PaginatedResult<CountryListItemCommand>>.Success(mappedResult);
            
        }
    }
}
