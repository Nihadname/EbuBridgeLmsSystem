namespace EbuBridgeLmsSystem.Application.Features.AuditLogFeature.Query.GetAllLogs
{
    public sealed record AuditLogListItemDto
    {
        public string TableName { get; init; }
        public string Action { get; init; }
        public string UserId { get; init; }
        public string UserName { get; init; }
        public DateTime Timestamp { get; init; } = DateTime.UtcNow;
        public string Changes { get; init; }
        public string ClientIpAddress { get; init; }
    }
}
