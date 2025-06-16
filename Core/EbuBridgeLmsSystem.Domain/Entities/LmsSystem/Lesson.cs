using LearningManagementSystem.Core.Entities.Common;

namespace EbuBridgeLmsSystem.Domain.Entities.LmsSystem
{
    public sealed class Lesson : BaseEntity
    {
        public string Title { get; set; }
        public LessonStatus Status { get; set; }
        public string Description { get; set; }
        public LessonType LessonType { get; set; }
        public string GradingPolicy { get; set; }
        public Guid CourseId { get; set; }
        public Course Course { get; set; }
        public ICollection<LessonStudentTeacher> LessonStudents { get; set; }
       
        public ICollection<LessonUnit> LessonUnits { get; set; }
        public ICollection<CourseTeacherLesson> CourseTeacherLessons { get; set; }

    }
    public enum LessonStatus
    {
        Scheduled,
        Completed,
        Canceled
    }
    public enum LessonType
    {
        Lecture,
        Lab,
        Tutorial,
        Online
    }
}
