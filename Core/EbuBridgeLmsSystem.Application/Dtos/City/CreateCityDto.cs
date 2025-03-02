namespace EbuBridgeLmsSystem.Application.Dtos.City
{
    public sealed record CreateCityDto
    {
        public string Name { get; init; }
        public Guid CountryId { get; init; }
    }
}
