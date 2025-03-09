using AutoMapper;
using EbuBridgeLmsSystem.Application.Features.AddressFeature.Commands.AddressCreate;
using EbuBridgeLmsSystem.Application.Features.AddressFeature.Commands.UpdateAdress;
using EbuBridgeLmsSystem.Application.Helpers.Methods;
using EbuBridgeLmsSystem.Domain.Entities;
using EbuBridgeLmsSystem.Domain.Entities.Common;
using EbuBridgeLmsSystem.Domain.Repositories;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EbuBridgeLmsSystem.Application.Features.AddressFeature.Commands.UpdateAddress
{
    public class UpdateAddressHandler : IRequestHandler<UpdateAddressCommand, Result<Unit>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateAddressHandler> _logger;

        public UpdateAddressHandler(ILogger<UpdateAddressHandler> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<Unit>> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
        {
            var existedAddress=await _unitOfWork.AddressRepository.GetEntity(s=>s.Id==request.Id,AsnoTracking:true);
            if (existedAddress is null)
                return Result<Unit>.Failure(Error.NotFound, null, ErrorType.NotFoundError);
            bool hasChanges = false;
            var hasCity = request.CityId.HasValue && request.CityId.Value != Guid.Empty;
            var hasCountry= request.CountryId.HasValue && request.CountryId.Value != Guid.Empty;
            var validationResult = await ValidateUpdateMethod(hasCity, hasCountry, request, existedAddress);
            if (hasCountry)
            {
                existedAddress.CountryId = request.CountryId.Value;
                hasChanges = true;
            }
            if (hasCity)
            {
                existedAddress.CityId = request.CityId.Value;
                hasChanges = true;
            }
            if (!string.IsNullOrWhiteSpace(request.Region) && request.Region != existedAddress.Region)
            {
                existedAddress.Region = request.Region;
                hasChanges = true;
            }
            if (!string.IsNullOrWhiteSpace(request.Street) && request.Region != existedAddress.Region)
            {
                existedAddress.Street = request.Street;
                hasChanges = true;
            }
            var isLocationExist = await AddressHelper.IsLocationExist(request);
            if (!isLocationExist)
                return Result<Unit>.Failure(Error.Custom("location", "location doesnt exist in the map"), null, ErrorType.NotFoundError);
            if (!hasChanges)
            {
                return Result<Unit>.Success(Unit.Value); 
            }
            await _unitOfWork.AddressRepository.Update(existedAddress);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Address with ID {AddressId} successfully updated.", request.Id);
            return Result<Unit>.Success(Unit.Value);
        }
        private async Task<Result<Unit>> ValidateUpdateMethod(bool hasCity,bool hasCountry,UpdateAddressCommand request, Address existingAddress)
        {
            if (hasCountry && !hasCity)
            {
                return Result<Unit>.Failure(Error.Custom(null, "CityId is required when CountryId is provided."), null, ErrorType.BusinessLogicError);
            }
            if (hasCountry && !await _unitOfWork.CountryRepository.isExists(s => s.Id == request.CountryId))
            {
                return Result<Unit>.Failure(Error.Custom("location", "country doesnt exist in the database or either your value is invalid or city is in diffrent  country"), null, ErrorType.NotFoundError);
            }
            if (hasCity && !await _unitOfWork.CityRepository.isExists(s => s.Id == request.CityId.Value && s.CountryId == (hasCountry ? request.CountryId.Value : existingAddress.CountryId)))
            {
                return Result<Unit>.Failure(Error.Custom("location", "city doesnt exist in the database or either your value is invalid or city is in diffrent  country"), null, ErrorType.NotFoundError);
            }
            return Result<Unit>.Success(Unit.Value);
        }
    }
}
