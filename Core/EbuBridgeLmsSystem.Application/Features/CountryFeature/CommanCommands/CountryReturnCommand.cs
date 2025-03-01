namespace EbuBridgeLmsSystem.Application.Features.CountryFeature.CommanCommands
{
    public sealed record CountryReturnCommand
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public DateTime? CreatedTime { get; init; }
        public List<CitiesinCountryListItemCommand> citiesinCountryListItemCommands { get; init; }
    }
}
