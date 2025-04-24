using EbuBridgeLmsSystem.Domain.Enums;
using LearningManagementSystem.Core.Entities.Common;

namespace EbuBridgeLmsSystem.Domain.Entities
{
    public sealed class Teacher:BaseEntity
    {
        public string Description { get; set; }
        public int Experience { get; set; }
        public Position Position { get; set; }
        public decimal Salary { get; set; }
        public string FaceBookUrl { get; set; }
        public string PinterestUrl { get; set; }
        public string SkypeUrl { get; set; }
        public string IntaUrl { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public ICollection<CourseTeacher> CourseTeachers { get; set; }
        public List<TeacherFacultyDegree> TeacherFacultyDegrees { get; set; }
        public ICollection<LessonStudentTeacher>  lessonStudentTeachers { get; set; }

    }
}
