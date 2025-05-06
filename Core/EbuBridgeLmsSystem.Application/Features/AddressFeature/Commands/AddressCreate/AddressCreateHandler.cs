using AutoMapper;
using EbuBridgeLmsSystem.Application.Helpers.Methods;
using EbuBridgeLmsSystem.Application.Interfaces;
using EbuBridgeLmsSystem.Domain.Entities.Common;
using EbuBridgeLmsSystem.Domain.Entities.LmsSystem;
using EbuBridgeLmsSystem.Domain.Repositories;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace EbuBridgeLmsSystem.Application.Features.AddressFeature.Commands.AddressCreate
{
    public class AddressCreateHandler : IRequestHandler<AddressCreateCommand, Result<Unit>>
    {
        private readonly   IAppUserResolver _userResolver;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AddressCreateHandler> _logger;
        public AddressCreateHandler(IAppUserResolver userResolver, IConfiguration configuration, HttpClient httpClient, IMapper mapper, IUnitOfWork unitOfWork, ILogger<AddressCreateHandler> logger)
        {
            _userResolver = userResolver;
            _configuration = configuration;
            _httpClient = httpClient;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Result<Unit>> Handle(AddressCreateCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                var currentUserInSystem = await _userResolver.GetCurrentUserAsync(includes: new Func<IQueryable<AppUser>, IQueryable<AppUser>>[]{
               query => query
            .Include(p => p.Address)
            });
                if (currentUserInSystem == null)
                    Result<Unit>.Failure(Error.Unauthorized, null, ErrorType.UnauthorizedError);
                if (currentUserInSystem.Address is not null)
                {
                    await _unitOfWork.AddressRepository.Delete(currentUserInSystem.Address);
                }
                var isExistedCountry = await _unitOfWork.CountryRepository.isExists(s => s.Id == request.CountryId && !s.IsDeleted);
                if(!isExistedCountry)
                    return Result<Unit>.Failure(Error.Custom("location", "country doesnt exist in the database or either your value is invalid"), null, ErrorType.NotFoundError);
                var isExistedCity = await _unitOfWork.CityRepository.isExists(s => s.Id == request.CityId&&s.CountryId==request.CountryId&&!s.IsDeleted);
                if (!isExistedCity)
                    return Result<Unit>.Failure(Error.Custom("location", "city doesnt exist in the database or either your value is invalid or city is in diffrent  country"), null, ErrorType.NotFoundError);
                var isLocationExist = await AddressHelper.IsLocationExist(request,_configuration,_httpClient,_unitOfWork);
                if (!isLocationExist)
                    return Result<Unit>.Failure(Error.Custom("location", "location doesnt exist in the map"), null, ErrorType.NotFoundError);
                request.AppUserId = currentUserInSystem.Id;
                var mappedAddress = _mapper.Map<Address>(request);
                await _unitOfWork.AddressRepository.Create(mappedAddress);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();
                return Result<Unit>.Success(Unit.Value,SuccessReturnType.Created);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                _logger.LogError(ex, "Error occurred during user address create");
                return Result<Unit>.Failure(Error.InternalServerError, null, ErrorType.SystemError);
            }
        }
       
    }
}
