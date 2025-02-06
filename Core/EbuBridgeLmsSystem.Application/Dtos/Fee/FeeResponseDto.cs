namespace EbuBridgeLmsSystem.Application.Dtos.Fee
{
    public sealed class FeeResponseDto
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public AppUserInFee Customer { get; set; }
        public string clientSecret { get; set; }
        public string Message { get; set; }
    }

    public sealed class AppUserInFee
    {
        public string UserName { get; set; }
    }
}
