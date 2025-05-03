namespace EbuBridgeLmsSystem.Application.Features.AddressFeature.Queries.GetAllAddresses
{
    public sealed record AddressListItemQuery
    {
        public Guid Id { get; init; }
        public string Country { get; init; }
        public string City { get; init; }
        public string Region { get; init; }
        public string Street { get; init; }
        public DateTime CreatedTime { get; init; }
        public AppUserInAdress AppUserInAdress { get; init; }
    }
    public sealed record AppUserInAdress
    {
        public string Id { get; init; }
        public string UserName { get; init; }
    }
}
