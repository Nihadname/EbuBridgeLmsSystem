namespace EbuBridgeLmsSystem.Application.Features.CountryFeature.CommanCommands
{
    public sealed record CountryReturnQuery
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public DateTime? CreatedTime { get; init; }
        public List<CitiesinCountryListItemCommand> citiesinCountryListItemCommands { get; init; }
    }
}
