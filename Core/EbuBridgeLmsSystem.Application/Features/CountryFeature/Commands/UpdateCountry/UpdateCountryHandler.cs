using AutoMapper;
using EbuBridgeLmsSystem.Domain.Entities.Common;
using EbuBridgeLmsSystem.Domain.Repositories;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EbuBridgeLmsSystem.Application.Features.CountryFeature.Commands.UpdateCountry
{
    public class UpdateCountryHandler : IRequestHandler<UpdateCountryCommand, Result<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateCountryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<Unit>> Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
        {
            var existedCountry=await _unitOfWork.CountryRepository.GetEntity(s=>s.Id==request.Id&!s.IsDeleted);
            if (existedCountry is null)
                return Result<Unit>.Failure(Error.NotFound, null, ErrorType.NotFoundError);
            var existedCountryWithThisName = await _unitOfWork.CountryRepository.GetEntity(s => EF.Functions.Like(s.Name, $"%{request.Name}%") && s.Id!=existedCountry.Id,AsnoTracking:true);
            if (existedCountryWithThisName is not null)
                return Result<Unit>.Failure(Error.DuplicateConflict, null, ErrorType.ValidationError);
            _mapper.Map(request, existedCountry);
           await  _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result<Unit>.Success(Unit.Value);
        }
    }
}
