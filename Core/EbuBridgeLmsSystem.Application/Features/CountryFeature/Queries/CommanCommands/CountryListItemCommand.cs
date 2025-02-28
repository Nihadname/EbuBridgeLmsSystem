namespace EbuBridgeLmsSystem.Application.Features.CountryFeature.Queries.CommanCommands
{
    public record CountryListItemCommand
    {
        public Guid Id { get; init; }
        public string Name  { get; init; }
        public bool IsDeleted { get; init; }
        public List<CitiesinCountryListItemCommand> citiesinCountryListItemCommands { get; init; }

    }
    public record CitiesinCountryListItemCommand {
        public Guid Id { get; init; }
    public string Name { get; init; }

    }

}
