namespace EbuBridgeLmsSystem.Application.Dtos.City
{
    public sealed record CityUpdateDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public Guid? CountryId { get; init; }
    }
}
