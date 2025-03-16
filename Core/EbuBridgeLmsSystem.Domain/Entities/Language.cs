using LearningManagementSystem.Core.Entities.Common;

namespace EbuBridgeLmsSystem.Domain.Entities
{
    public sealed class Language:BaseEntity
    {
        public string Name { get; set; }
        public Course Course { get; set; }
    }
}
