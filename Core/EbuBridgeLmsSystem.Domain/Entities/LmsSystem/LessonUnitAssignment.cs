using LearningManagementSystem.Core.Entities.Common;

namespace EbuBridgeLmsSystem.Domain.Entities.LmsSystem
{
    public sealed class LessonUnitAssignment : BaseEntity
    {
        public Guid LessonUnitId { get; set; }
        public LessonUnit LessonUnit { get; set; }
        public Guid StudentId { get; set; }
        public Student Student { get; set; }
        public Guid TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public DateTime ScheduledStartTime { get; set; }
        public DateTime ScheduledEndTime { get; set; }
        public TimeSpan Duration { get; set; }
        public Meeting LessonMeeting { get; set; }
        public LessonUnitAttendance lessonUnitAttendance { get; set; }



        public void SetDurationAutomaticly()
        {
            if (ScheduledEndTime > ScheduledStartTime)
                Duration = ScheduledEndTime - ScheduledStartTime;
            else
                Duration = TimeSpan.Zero;
        }
        public class Meeting
        {
            public string Link { get; set; }
            public bool IsVerified { get; set; }
        }
    }
}
