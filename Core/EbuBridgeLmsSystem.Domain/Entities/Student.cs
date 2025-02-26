using LearningManagementSystem.Core.Entities.Common;

namespace EbuBridgeLmsSystem.Domain.Entities
{
    public sealed class Student:BaseEntity
    { 
        public decimal? AvarageScore { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public Guid? ParentId { get; set; }
        public Parent Parent { get; set; }
        public ICollection<LessonStudent> lessonStudents { get; set; }
        public ICollection<CourseStudent> courseStudents { get; set; }
        public ICollection<Fee> fees { get; set; }
        public bool IsEnrolled { get; set; }

    }
}
