using LearningManagementSystem.Core.Entities.Common;

namespace EbuBridgeLmsSystem.Domain.Entities
{
    public sealed class Report:BaseEntity
    {
        public string Description { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public string ReportedAppUserId { get; set; }  
        public AppUser ReportedAppUser { get; set; }
        public Guid ReportOptionId { get; set; }
        public ReportOption ReportOption { get; set; }
        public bool IsVerified { get; set; }
    }
}
