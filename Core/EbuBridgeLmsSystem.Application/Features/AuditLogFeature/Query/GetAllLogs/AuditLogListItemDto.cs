namespace EbuBridgeLmsSystem.Application.Features.AuditLogFeature.Query.GetAllLogs
{
    public class AuditLogListItemDto
    {
        public string TableName { get; set; }
        public string Action { get; set; }
        public string UserId { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string Changes { get; set; }
        public string ClientIpAddress { get; set; }
    }
}
