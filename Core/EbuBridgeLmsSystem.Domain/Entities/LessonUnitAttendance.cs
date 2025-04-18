using LearningManagementSystem.Core.Entities.Common;

namespace EbuBridgeLmsSystem.Domain.Entities
{
    public sealed class LessonUnitAttendance:BaseEntity
    {
        public Guid lessonUnitAssignmentId { get; set; }  
        public LessonUnitAssignment lessonUnitAssignment { get; set; }
        public DateTime AttendanceDate { get; set; }
        public bool IsPresent { get; set; }
    }
}
