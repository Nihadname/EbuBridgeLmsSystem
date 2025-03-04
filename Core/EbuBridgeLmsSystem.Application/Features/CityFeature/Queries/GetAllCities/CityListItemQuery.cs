namespace EbuBridgeLmsSystem.Application.Features.CityFeature.Queries.GetAllCities
{
    public sealed record CityListItemQuery
    {
        public Guid Id  { get; init; }
        public string Name { get; init; }
        public CountryInCityListItemQuery   countryInCityListItemQuery  { get; init; }
    }
    public class CountryInCityListItemQuery
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
    }
}
