using AutoMapper;
using EbuBridgeLmsSystem.Domain.Entities;
using EbuBridgeLmsSystem.Domain.Entities.Common;
using EbuBridgeLmsSystem.Domain.Repositories;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;

namespace EbuBridgeLmsSystem.Application.Features.CityFeature.Commands.CreateCity
{
    public class CreateCityHandler : IRequestHandler<CreateCityCommand, Result<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateCityHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<Unit>> Handle(CreateCityCommand request, CancellationToken cancellationToken)
        {
            var existedCity= await _unitOfWork.CityRepository.GetEntity(s => s.Name.ToLower() == request.Name.ToLower(),AsnoTracking:true);
            if (existedCity is not null)
            {
                if (existedCity.IsDeleted)
                {
                    existedCity.IsDeleted = false;
                    await _unitOfWork.SaveChangesAsync(cancellationToken);
                    return Result<Unit>.Success(Unit.Value);
                }
                return Result<Unit>.Failure(Error.DuplicateConflict, null, ErrorType.ValidationError);
            }
            var isExistedCountry=await _unitOfWork.CountryRepository.isExists(s=>s.Id==request.CountryId&&!s.IsDeleted);
            if (!isExistedCountry)
                return Result<Unit>.Failure(Error.Custom("country","invalid id"), null, ErrorType.ValidationError);
            var mappedCity = _mapper.Map<City>(request);
            await _unitOfWork.CityRepository.Create(mappedCity);
            await _unitOfWork.SaveChangesAsync();
            return Result<Unit>.Success(Unit.Value);
        }
    }
}
