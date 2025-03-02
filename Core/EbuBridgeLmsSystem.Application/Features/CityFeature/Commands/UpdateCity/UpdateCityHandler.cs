using AutoMapper;
using EbuBridgeLmsSystem.Domain.Entities.Common;
using EbuBridgeLmsSystem.Domain.Repositories;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;

namespace EbuBridgeLmsSystem.Application.Features.CityFeature.Commands.UpdateCity
{
    public class UpdateCityHandler : IRequestHandler<UpdateCityCommand, Result<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateCityHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<Unit>> Handle(UpdateCityCommand request, CancellationToken cancellationToken)
        {
            var existedCity=await _unitOfWork.CityRepository.GetEntity(s=>s.Id==request.Id,true);
            if (existedCity is null)
                return Result<Unit>.Failure(Error.NotFound, null, ErrorType.NotFoundError);
            var isExistedCityName=await _unitOfWork.CityRepository.isExists(s=>s.Name.ToLower()==request.Name.ToLower());
            if (!string.IsNullOrWhiteSpace(request.Name)&& isExistedCityName)
            {
                return Result<Unit>.Failure(Error.Custom("City Name", "it already exists"), null, ErrorType.BusinessLogicError);
            }
            var isExistedCountry = await _unitOfWork.CountryRepository.isExists(s => s.Id == request.CountryId);
            if (request.CountryId != Guid.Empty&&!isExistedCountry)
            {
              return Result<Unit>.Failure(Error.Custom("country", "invalid id"), null, ErrorType.NotFoundError);
            }
            _mapper.Map(request, existedCity);
            await _unitOfWork.CityRepository.Update(existedCity);
            await _unitOfWork.SaveChangesAsync();
            return Result<Unit>.Success(Unit.Value);
        }
    }
}
