namespace EbuBridgeLmsSystem.Application.Dtos.Auth
{
    public record AuthGetEmailBodyDto
    {
        public string Token { get; init; }
        public string ApiEndpoint { get; init; }
    }
}
