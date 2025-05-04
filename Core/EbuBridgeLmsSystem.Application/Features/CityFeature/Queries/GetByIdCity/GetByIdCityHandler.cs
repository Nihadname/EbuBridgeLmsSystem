using EbuBridgeLmsSystem.Application.Features.CityFeature.Queries.GetAllCities;
using EbuBridgeLmsSystem.Domain.Entities.Common;
using EbuBridgeLmsSystem.Domain.Repositories;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EbuBridgeLmsSystem.Application.Features.CityFeature.Queries.GetByIdCity
{
    public class GetByIdCityHandler : IRequestHandler<GetByIdCityQuery, Result<CityReturnQuery>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetByIdCityHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<CityReturnQuery>> Handle(GetByIdCityQuery request, CancellationToken cancellationToken)
        {
            var existedCity = await  _unitOfWork.CityRepository.GetSelected(s=> new CityReturnQuery()
            {
                Id = s.Id,
                Name=s.Name,
                countryInCityListItemQuery=new CountryInCityListItemQuery()
                {
                    Id=s.CountryId,
                    Name=s.Country.Name,
                }
            }).FirstOrDefaultAsync(s=>s.Id==request.Id,cancellationToken);
            if (existedCity == null)
                return Result<CityReturnQuery>.Failure(Error.NotFound, null, ErrorType.NotFoundError);
            return Result<CityReturnQuery>.Success(existedCity, null);
        }
    }
}
