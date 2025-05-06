using LearningManagementSystem.Core.Entities.Common;

namespace EbuBridgeLmsSystem.Domain.Entities.LmsSystem
{
    public sealed class LessonUnitMaterial : BaseEntity
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public Guid LessonUnitId { get; set; }
        public LessonUnit LessonUnit { get; set; }
    }
}
