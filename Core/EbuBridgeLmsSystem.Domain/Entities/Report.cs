using LearningManagementSystem.Core.Entities.Common;

namespace EbuBridgeLmsSystem.Domain.Entities
{
    public class Report:BaseEntity
    {
        public string Description { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string ReportedUserId { get; set; }  
        public User ReportedUser { get; set; }
        public Guid ReportOptionId { get; set; }
        public ReportOption ReportOption { get; set; }
        public bool IsVerified { get; set; }
    }
}
