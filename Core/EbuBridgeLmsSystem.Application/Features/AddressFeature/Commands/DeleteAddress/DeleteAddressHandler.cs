using EbuBridgeLmsSystem.Domain.Entities.Common;
using EbuBridgeLmsSystem.Domain.Repositories;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;

namespace EbuBridgeLmsSystem.Application.Features.AddressFeature.Commands.DeleteAddress
{
    public class DeleteAddressHandler : IRequestHandler<DeleteAddressCommand, Result<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAddressHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
        {
            var existedAddress = await _unitOfWork.AddressRepository.GetEntity(s => s.Id == request.Id && !s.IsDeleted);
            if (existedAddress is null)
            {
                return Result<Unit>.Failure(Error.NotFound, null, ErrorType.NotFoundError);
            }
            if (existedAddress.IsDeleted is true)
                return Result<Unit>.Failure(Error.BadRequest, null, ErrorType.ValidationError);
            await _unitOfWork.AddressRepository.Delete(existedAddress);
            await _unitOfWork.SaveChangesAsync();
            return Result<Unit>.Success(Unit.Value,SuccessReturnType.NoContent);
        }
    }
}
