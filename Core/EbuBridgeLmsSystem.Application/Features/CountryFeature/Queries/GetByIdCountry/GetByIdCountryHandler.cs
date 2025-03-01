using AutoMapper;
using EbuBridgeLmsSystem.Application.Features.CountryFeature.CommanCommands;
using EbuBridgeLmsSystem.Domain.Entities.Common;
using EbuBridgeLmsSystem.Domain.Repositories;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;

namespace EbuBridgeLmsSystem.Application.Features.CountryFeature.Queries.GetByIdCountry
{
    public class GetByIdCountryHandler : IRequestHandler<GetByIdCountryQuery, Result<CountryReturnCommand>>
    {
        private  readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetByIdCountryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<CountryReturnCommand>> Handle(GetByIdCountryQuery request, CancellationToken cancellationToken)
        {
            var existedCountry=await _unitOfWork.CountryRepository.GetEntity(s=>s.Id==request.Id);
            if (existedCountry == null)
                return Result<CountryReturnCommand>.Failure(Error.NotFound, null, ErrorType.NotFoundError);
            var mappedCountry=_mapper.Map<CountryReturnCommand>(existedCountry);
            return Result<CountryReturnCommand>.Success(mappedCountry);
        }
    }
}
