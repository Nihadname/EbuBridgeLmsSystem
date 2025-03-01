namespace EbuBridgeLmsSystem.Application.Features.CountryFeature.CommanCommands
{
    public sealed record CountryListItemCommand
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public bool IsDeleted { get; init; }
        public List<CitiesinCountryListItemCommand> citiesinCountryListItemCommands { get; init; }

    }
    public record CitiesinCountryListItemCommand
    {
        public Guid Id { get; init; }
        public string Name { get; init; }

    }

}
