using LearningManagementSystem.Core.Entities.Common;

namespace EbuBridgeLmsSystem.Domain.Entities
{
    public sealed class LessonUnitStudentHomework:BaseEntity
    {
        public string Title { get; set; }                 
        public string Description { get; set; }             
        public DateTime AssignedDate { get; set; }  
        public DateTime DueDate { get; set; }             
        public ICollection<LessonHomeworkLink> Links { get; set; }
        public ICollection<LessonUnitStudentHomeworkMaterial> Materials { get; set; }
        public Guid LessonUnitId { get; set; }
        public LessonUnit LessonUnit { get; set; }
        public Guid StudentId { get; set; }
        public Student Student { get; set; }
        public ICollection<LessonUnitStudentHomeworkSubmission> lessonUnitStudentHomeworkSubmissions { get; set; }
    }
}
