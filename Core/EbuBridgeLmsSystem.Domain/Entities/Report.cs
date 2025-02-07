using LearningManagementSystem.Core.Entities.Common;

namespace EbuBridgeLmsSystem.Domain.Entities
{
    public class Report:BaseEntity
    {
        public string Description { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid ReportedUserId { get; set; }  
        public User ReportedUser { get; set; }
        public Guid ReportOptionId { get; set; }
        public ReportOption ReportOption { get; set; }
        public bool IsVerified { get; set; }
    }
}
