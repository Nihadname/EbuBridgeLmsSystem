using LearningManagementSystem.Core.Entities.Common;

namespace EbuBridgeLmsSystem.Domain.Entities
{
    public sealed class ReportOption:BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Report> reports { get; set; }
    }
}
