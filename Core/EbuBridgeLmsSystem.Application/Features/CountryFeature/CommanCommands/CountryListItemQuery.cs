namespace EbuBridgeLmsSystem.Application.Features.CountryFeature.CommanCommands
{
    public sealed record CountryListItemQuery
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public bool IsDeleted { get; init; }
        public DateTime CreatedTime { get; init; }
        public List<CitiesinCountryListItemCommand> citiesinCountryListItemCommands { get; init; }

    }
    public record CitiesinCountryListItemCommand
    {
        public Guid Id { get; init; }
        public string Name { get; init; }

    }

}
