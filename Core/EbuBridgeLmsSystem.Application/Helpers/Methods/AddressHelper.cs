using EbuBridgeLmsSystem.Application.Features.AddressFeature.Commands.AddressCreate;
using EbuBridgeLmsSystem.Application.Features.AddressFeature.Commands.Common;
using EbuBridgeLmsSystem.Domain.Entities;
using EbuBridgeLmsSystem.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace EbuBridgeLmsSystem.Application.Helpers.Methods
{
    public static class AddressHelper
    {
        private static IConfiguration _configuration;
        private static HttpClient _httpClient;
        private static IUnitOfWork _unitOfWork;
        public static async Task<bool> IsLocationExist(AddressBaseCommand addressBaseCommand )
        {
            var apiKey = _configuration.GetSection("MapApiKey").Value;
            var CityLocation = await _unitOfWork.CityRepository.GetEntity(s => s.Id == addressBaseCommand.CityId, includes: new Func<IQueryable<City>, IQueryable<City>>[] {
                 query => query
            .Include(p => p.Country) });
            if (CityLocation == null || CityLocation.Country == null)
                return false;
            var url = $"https://us1.locationiq.com/v1/search?key={apiKey}&q={CityLocation.Country.Name}%20{addressBaseCommand.Region}%20{CityLocation.Name}%20{addressBaseCommand.Street}&format=json";

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
