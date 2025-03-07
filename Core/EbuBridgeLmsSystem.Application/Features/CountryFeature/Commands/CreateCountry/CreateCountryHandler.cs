using AutoMapper;
using EbuBridgeLmsSystem.Domain.Entities;
using EbuBridgeLmsSystem.Domain.Entities.Common;
using EbuBridgeLmsSystem.Domain.Repositories;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EbuBridgeLmsSystem.Application.Features.CountryFeature.Commands.CreateCountry
{
    public class CreateCountryHandler : IRequestHandler<CreateCountryCommand, Result<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateCountryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<Unit>> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
        {
            var existedCountry = await _unitOfWork.CountryRepository.GetEntity(s => s.Name.ToLower() == request.Name.ToLower(),AsnoTracking:true,isIgnoredDeleteBehaviour:true);
            if (existedCountry is not null)
            {
                if (existedCountry.IsDeleted)
                {
                    existedCountry.IsDeleted = false;
                    await _unitOfWork.SaveChangesAsync(cancellationToken);
                    return Result<Unit>.Success(Unit.Value);
                }
                return Result<Unit>.Failure(Error.DuplicateConflict, null, ErrorType.ValidationError);
            }
        var mappedCountry=_mapper.Map<Country>(request);
            await _unitOfWork.CountryRepository.Create(mappedCountry);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result<Unit>.Success(Unit.Value);
        }
    }
}
