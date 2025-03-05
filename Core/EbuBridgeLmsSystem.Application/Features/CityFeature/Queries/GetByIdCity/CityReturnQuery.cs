using EbuBridgeLmsSystem.Application.Features.CityFeature.Queries.GetAllCities;

namespace EbuBridgeLmsSystem.Application.Features.CityFeature.Queries.GetByIdCity
{
    public sealed  record CityReturnQuery
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public CountryInCityListItemQuery countryInCityListItemQuery { get; init; }
    }
}
