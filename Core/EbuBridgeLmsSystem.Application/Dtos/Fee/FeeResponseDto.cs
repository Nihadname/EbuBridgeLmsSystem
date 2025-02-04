namespace EbuBridgeLmsSystem.Application.Dtos.Fee
{
    public class FeeResponseDto
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public AppUserInFee Customer { get; set; }
        public string clientSecret { get; set; }
        public string Message { get; set; }
    }

    public class AppUserInFee
    {
        public string UserName { get; set; }
    }
}
