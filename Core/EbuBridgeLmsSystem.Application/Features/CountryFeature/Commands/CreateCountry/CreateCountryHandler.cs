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
            var isCountryExist = await _unitOfWork.CountryRepository.isExists(s => s.Name.ToLower() == request.Name.ToLower(),AsNoTracking:true,isIgnoredDeleteBehaviour:true);
           if(isCountryExist)
                return Result<Unit>.Failure(Error.DuplicateConflict,null,ErrorType.ValidationError);
        var mappedCountry=_mapper.Map<Country>(request);
            await _unitOfWork.CountryRepository.Create(mappedCountry);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result<Unit>.Success(Unit.Value);
        }
    }
}
