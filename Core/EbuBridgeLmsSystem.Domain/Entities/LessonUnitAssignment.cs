using LearningManagementSystem.Core.Entities.Common;

namespace EbuBridgeLmsSystem.Domain.Entities
{
    public sealed class LessonUnitAssignment:BaseEntity
    {
        public Guid LessonUnitId { get; set; }
        public LessonUnit LessonUnit { get; set; }

        public Guid StudentId { get; set; }
        public Student Student { get; set; }

        public DateTime ScheduledStartTime { get; set; }
        public DateTime ScheduledEndTime { get; set; }
    }
}
