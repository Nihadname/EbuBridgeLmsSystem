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
      
        public UpdateCityHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(UpdateCityCommand request, CancellationToken cancellationToken)
        {
            var existedCity=await _unitOfWork.CityRepository.GetEntity(s=>s.Id==request.Id&&!s.IsDeleted,true);
            if (existedCity is null)
                return Result<Unit>.Failure(Error.NotFound, null, ErrorType.NotFoundError);
            bool hasChanges = false;
            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                 var existedCityWithThisName = await _unitOfWork.CityRepository.GetEntity(
                      s => s.Name.Equals(request.Name, StringComparison.OrdinalIgnoreCase)&& s.Id!=existedCity.Id, AsnoTracking: true);
                if (existedCityWithThisName is not null)
                {
                    if (existedCityWithThisName.IsDeleted)
                    {
                        return Result<Unit>.Failure(Error.Custom("City Name", "This city was previously deleted. Consider renaming the existing record instead of restoring."), null, ErrorType.BusinessLogicError);
                    }
                    return Result<Unit>.Failure(Error.Custom("City Name", "it already exists"), null, ErrorType.BusinessLogicError);
                }
                existedCity.Name=request.Name;
                hasChanges = true;
            }
            if (request.CountryId.HasValue && request.CountryId.Value != Guid.Empty)
            {
                var isExistedCountry = await _unitOfWork.CountryRepository.isExists(s => s.Id == request.CountryId && !s.IsDeleted);
                if (!isExistedCountry)
                {
                    return Result<Unit>.Failure(Error.Custom("country", "invalid id"), null, ErrorType.NotFoundError);
                }
                existedCity.CountryId = (Guid)request.CountryId;
                hasChanges =true;   
            }

            if (hasChanges)
            {
                await _unitOfWork.CityRepository.Update(existedCity);
                await _unitOfWork.SaveChangesAsync();
            }
            return Result<Unit>.Success(Unit.Value, null);
        }
    }
}
