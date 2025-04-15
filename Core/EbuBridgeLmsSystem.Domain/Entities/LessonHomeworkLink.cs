using LearningManagementSystem.Core.Entities.Common;

namespace EbuBridgeLmsSystem.Domain.Entities
{
    public sealed class LessonHomeworkLink:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid LessonUnitId { get; set; }
        public LessonUnit LessonUnit { get; set; }
    }
}
