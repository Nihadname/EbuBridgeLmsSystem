using LearningManagementSystem.Core.Entities.Common;

namespace EbuBridgeLmsSystem.Domain.Entities
{
    public sealed class LessonStudentTeacher:BaseEntity
    {
        public Guid LessonId  { get; set; }
        public Lesson Lesson { get; set; }
        public Guid StudentId { get; set; }
        public Student Student { get; set; }
        public Guid? TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public ICollection<QuizResult> QuizResults { get; set; }
        public ICollection<LessonUnitAttendance> lessonUnitAttendances { get; set; }
        public bool isFinished { get; set; }
        public bool isApproved { get; set; }

    }
}
