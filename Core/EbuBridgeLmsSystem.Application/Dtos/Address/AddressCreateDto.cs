namespace EbuBridgeLmsSystem.Application.Dtos.Address
{
    public sealed class AddressCreateDto
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Street { get; set; }
        public string AppUserId { get; set; }
    }      
}
