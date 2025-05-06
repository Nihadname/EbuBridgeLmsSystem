using LearningManagementSystem.Core.Entities.Common;

namespace EbuBridgeLmsSystem.Domain.Entities.LmsSystem
{
    public sealed class AuditLog : BaseEntity
    {

        public string TableName { get; set; }
        public string Action { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string Changes { get; set; }
        public string ClientIpAddress { get; set; }
    }
}
