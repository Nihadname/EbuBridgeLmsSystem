using EbuBridgeLmsSystem.Domain.Entities.Common;
using EbuBridgeLmsSystem.Domain.Repositories;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Features.CountryFeature.Commands.DeleteCountry
{
    public class DeleteCountryHandler : IRequestHandler<DeleteCountryCommand, Result<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCountryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(DeleteCountryCommand request, CancellationToken cancellationToken)
        {
            var existedCountry = await _unitOfWork.CountryRepository.GetEntity(s => s.Id == request.Id);
            if (existedCountry is null)
                return Result<Unit>.Failure(Error.NotFound, null, ErrorType.NotFoundError);
            if(existedCountry.IsDeleted is true)
                return Result<Unit>.Failure(Error.BadRequest, null, ErrorType.ValidationError);
            await _unitOfWork.CountryRepository.Delete(existedCountry);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result<Unit>.Success(Unit.Value);

        }
    }
}
