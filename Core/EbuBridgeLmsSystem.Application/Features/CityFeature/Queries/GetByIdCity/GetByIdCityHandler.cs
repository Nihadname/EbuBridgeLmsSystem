using AutoMapper;
using EbuBridgeLmsSystem.Application.Features.CountryFeature.CommanCommands;
using EbuBridgeLmsSystem.Domain.Entities;
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
        private readonly IMapper _mapper;
        public GetByIdCityHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<CityReturnQuery>> Handle(GetByIdCityQuery request, CancellationToken cancellationToken)
        {
            var existedCity = await _unitOfWork.CityRepository.GetEntity(s => s.Id == request.Id && !s.IsDeleted,true, includes: new Func<IQueryable<City>, IQueryable<City>>[] {
                 query => query
            .Include(p => p.Country) });
            if (existedCity == null)
                return Result<CityReturnQuery>.Failure(Error.NotFound, null, ErrorType.NotFoundError);
            var mappedCountry = _mapper.Map<CityReturnQuery>(existedCity);
            return Result<CityReturnQuery>.Success(mappedCountry, null);
        }
    }
}
