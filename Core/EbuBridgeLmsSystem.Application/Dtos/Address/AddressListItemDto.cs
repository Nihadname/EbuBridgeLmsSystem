namespace EbuBridgeLmsSystem.Application.Dtos.Address
{
    public sealed record AddressListItemDto
    {
        public string Country { get; init; }
        public string City { get; init; }
        public string Region { get; init; }
        public string Street { get; set; }
        public AppUserInAdress appUserInAdress { get; init; }
    }
}
