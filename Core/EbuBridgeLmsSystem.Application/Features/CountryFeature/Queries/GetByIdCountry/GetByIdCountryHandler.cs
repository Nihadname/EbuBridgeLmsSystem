using AutoMapper;
using EbuBridgeLmsSystem.Application.Features.CountryFeature.CommanCommands;
using EbuBridgeLmsSystem.Domain.Entities.Common;
using EbuBridgeLmsSystem.Domain.Repositories;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EbuBridgeLmsSystem.Application.Features.CountryFeature.Queries.GetByIdCountry
{
    public class GetByIdCountryHandler : IRequestHandler<GetByIdCountryQuery, Result<CountryReturnQuery>>
    {
        private  readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetByIdCountryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<CountryReturnQuery>> Handle(GetByIdCountryQuery request, CancellationToken cancellationToken)
        {
            var existedCountry=await _unitOfWork.CountryRepository.GetSelected(s=> new CountryReturnQuery()
            {
                Id = s.Id,
               CreatedTime=s.CreatedTime,
               Name = s.Name,
               citiesinCountryListItemCommands=s.Cities.Select(s=>new CitiesinCountryListItemCommand()
               {
                   Id = s.Id,
                   Name = s.Name,
               }).Take(2).ToList()
            }).FirstOrDefaultAsync(s=>s.Id==request.Id,cancellationToken);
            if (existedCountry == null)
                return Result<CountryReturnQuery>.Failure(Error.NotFound, null, ErrorType.NotFoundError);
            return Result<CountryReturnQuery>.Success(existedCountry, null);
        }
    }
}
