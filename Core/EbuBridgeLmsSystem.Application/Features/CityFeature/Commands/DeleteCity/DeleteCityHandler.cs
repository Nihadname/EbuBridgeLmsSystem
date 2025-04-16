using EbuBridgeLmsSystem.Domain.Entities.Common;
using EbuBridgeLmsSystem.Domain.Repositories;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;

namespace EbuBridgeLmsSystem.Application.Features.CityFeature.Commands.DeleteCity
{
    public class DeleteCityHandler : IRequestHandler<DeleteCityCommand, Result<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCityHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(DeleteCityCommand request, CancellationToken cancellationToken)
        {
            var existedCity=await _unitOfWork.CityRepository.GetEntity(s=>s.Id==request.Id&&!s.IsDeleted);
            if(existedCity is null)
            {
                return Result<Unit>.Failure(Error.NotFound, null, ErrorType.NotFoundError);
            }
            if (existedCity.IsDeleted is true)
                return Result<Unit>.Failure(Error.BadRequest, null, ErrorType.ValidationError);
            await _unitOfWork.CityRepository.Delete(existedCity);
            await _unitOfWork.SaveChangesAsync();
            return Result<Unit>.Success(Unit.Value, null);
            
        }
    }
}
