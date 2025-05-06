using LearningManagementSystem.Core.Entities.Common;

namespace EbuBridgeLmsSystem.Domain.Entities.LmsSystem
{
    public sealed class CourseTeacherLesson : BaseEntity
    {
        public Guid CourseTeacherId { get; set; }
        public CourseTeacher CourseTeacher { get; set; }
        public Guid LessonId { get; set; }
        public Lesson Lesson { get; set; }
    }
}
