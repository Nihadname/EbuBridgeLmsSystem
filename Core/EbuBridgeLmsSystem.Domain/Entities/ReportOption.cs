using LearningManagementSystem.Core.Entities.Common;

namespace EbuBridgeLmsSystem.Domain.Entities
{
    public class ReportOption:BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Report> reports { get; set; }
    }
}
