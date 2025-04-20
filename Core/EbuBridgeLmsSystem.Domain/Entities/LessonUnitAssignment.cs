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
        public TimeSpan Duration { get; set; }
        public string MeetingLink { get; set; }
        public LessonUnitAttendance lessonUnitAttendance { get; set; }



        public void SetDurationAutomaticly()
        {
            if (ScheduledEndTime > ScheduledStartTime)
                this.Duration = ScheduledEndTime - ScheduledStartTime;
            else
                this.Duration = TimeSpan.Zero;
        }
    }
}
