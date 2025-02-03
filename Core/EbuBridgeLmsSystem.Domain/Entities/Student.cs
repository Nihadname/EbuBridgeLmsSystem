using LearningManagementSystem.Core.Entities.Common;

namespace EbuBridgeLmsSystem.Domain.Entities
{
    public class Student:BaseEntity
    { 
        public decimal? AvarageScore { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public Guid? ParentId { get; set; }
        public Parent Parent { get; set; }
        public ICollection<LessonStudent> lessonStudents { get; set; }
        public ICollection<CourseStudent> courseStudents { get; set; }
        public ICollection<Fee> fees { get; set; }
        public bool IsEnrolled { get; set; }

    }
}
