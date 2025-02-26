using AutoMapper;
using EbuBridgeLmsSystem.Application.Interfaces;
using EbuBridgeLmsSystem.Domain.Entities;
using EbuBridgeLmsSystem.Domain.Entities.Common;
using EbuBridgeLmsSystem.Domain.Repositories;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
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
        public AddressCreateHandler(IAppUserResolver userResolver, UserManager<AppUser> userManager, IConfiguration configuration, HttpClient httpClient, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _userResolver = userResolver;
            _userManager = userManager;
            _configuration = configuration;
            _httpClient = httpClient;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(AddressCreateCommand request, CancellationToken cancellationToken)
        {
            var currentUserInSystem = await _userResolver.GetCurrentUserAsync();
            if (currentUserInSystem == null)
                Result<Unit>.Failure(Error.Unauthorized,null, ErrorType.UnauthorizedError);
            if (currentUserInSystem.Address is not null)
                Result<Unit>.Failure(Error.Custom("Address", "User already has an address. Update or delete the existing address instead."), null, ErrorType.SystemError);
            var isLocationExist = await IsLocationExist(request);
            if(!isLocationExist)
                return Result<Unit>.Failure(Error.Custom("location", "location doesnt exist in the map"),null, ErrorType.NotFoundError);
            request.AppUserId = currentUserInSystem.Id;
            var mappedAddress= _mapper.Map<Address>(request);
            await _unitOfWork.AddressRepository.Create(mappedAddress);
            await _unitOfWork.SaveChangesAsync();
            return Result<Unit>.Success(Unit.Value);
        }
        private async Task<bool> IsLocationExist(AddressCreateCommand addressCreateDto)
        {
            var apiKey = _configuration.GetSection("MapApiKey").Value;

            var url = $"https://us1.locationiq.com/v1/search?key={apiKey}&q={addressCreateDto.Country}%20{addressCreateDto.Region}%20{addressCreateDto.City}%20{addressCreateDto.Street}&format=json";

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
