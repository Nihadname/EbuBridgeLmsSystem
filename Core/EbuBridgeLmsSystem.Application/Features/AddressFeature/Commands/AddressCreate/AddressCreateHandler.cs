using AutoMapper;
using EbuBridgeLmsSystem.Application.Dtos.Auth;
using EbuBridgeLmsSystem.Application.Interfaces;
using EbuBridgeLmsSystem.Domain.Entities;
using EbuBridgeLmsSystem.Domain.Entities.Common;
using EbuBridgeLmsSystem.Domain.Repositories;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace EbuBridgeLmsSystem.Application.Features.AddressFeature.Commands.AddressCreate
{
    public class AddressCreateHandler : IRequestHandler<AddressCreateCommand, Result<Unit>>
    {
        private readonly   IAppUserResolver _userResolver;
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AddressCreateHandler> _logger;
        public AddressCreateHandler(IAppUserResolver userResolver, UserManager<AppUser> userManager, IConfiguration configuration, HttpClient httpClient, IMapper mapper, IUnitOfWork unitOfWork, ILogger<AddressCreateHandler> logger)
        {
            _userResolver = userResolver;
            _userManager = userManager;
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
                var isExistedCity = await _unitOfWork.CityRepository.isExists(s => s.Id == request.CityId);
                if (!isExistedCity)
                    return Result<Unit>.Failure(Error.Custom("location", "city doesnt exist in the database or either your value is invalid"), null, ErrorType.NotFoundError);
                var isLocationExist = await IsLocationExist(request);
                if (!isLocationExist)
                    return Result<Unit>.Failure(Error.Custom("location", "location doesnt exist in the map"), null, ErrorType.NotFoundError);
                request.AppUserId = currentUserInSystem.Id;
                var mappedAddress = _mapper.Map<Address>(request);
                await _unitOfWork.AddressRepository.Create(mappedAddress);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();
                return Result<Unit>.Success(Unit.Value);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                _logger.LogError(ex, "Error occurred during user address create");
                return Result<Unit>.Failure(Error.InternalServerError, null, ErrorType.SystemError);
            }
        }
        private async Task<bool> IsLocationExist(AddressCreateCommand addressCreateDto)
        {
            var apiKey = _configuration.GetSection("MapApiKey").Value;
            var CityLocation=await _unitOfWork.CityRepository.GetEntity(s=>s.Id==addressCreateDto.CityId, includes: new Func<IQueryable<City>, IQueryable<City>>[] {
                 query => query
            .Include(p => p.Country) });
            if (CityLocation == null || CityLocation.Country == null)
                return false;
            var url = $"https://us1.locationiq.com/v1/search?key={apiKey}&q={CityLocation.Country.Name}%20{addressCreateDto.Region}%20{CityLocation.Name}%20{addressCreateDto.Street}&format=json";

            try
            {
                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }

                var content = await response.Content.ReadAsStringAsync();

                var locations = JsonDocument.Parse(content).RootElement;

                if (locations.ValueKind == JsonValueKind.Array && locations.GetArrayLength() > 0)
                {
                    foreach (var location in locations.EnumerateArray())
                    {
                        if (location.TryGetProperty("place_id", out _) &&
                            location.TryGetProperty("lat", out _) &&
                            location.TryGetProperty("lon", out _))
                        {
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            return false;
        }
    }
}
