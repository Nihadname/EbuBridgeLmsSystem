namespace EbuBridgeLmsSystem.Application.Features.AddressFeature.Commands.Common
{
    public abstract  class AddressBaseCommand
    {
        public Guid? CityId { get; set; }
        public Guid? CountryId { get; set; }
        public string Region { get; set; }
        public string Street { get; set; }
    }
}
