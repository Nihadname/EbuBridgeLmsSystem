using LearningManagementSystem.Core.Entities.Common;

namespace EbuBridgeLmsSystem.Domain.Entities
{
    public sealed class LessonStudent:BaseEntity
    {
        public Guid LessonId  { get; set; }
        public Lesson Lesson { get; set; }
        public Guid StudentId { get; set; }
        public Student Student { get; set; }
        public ICollection<QuizResult> QuizResults { get; set; }
        public ICollection<LessonUnitAttendance> lessonUnitAttendances { get; set; }
        public bool isFinished { get; set; }

    }
}
