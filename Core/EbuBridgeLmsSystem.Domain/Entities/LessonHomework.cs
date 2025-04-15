using LearningManagementSystem.Core.Entities.Common;

namespace EbuBridgeLmsSystem.Domain.Entities
{
    public sealed class LessonHomework:BaseEntity
    {
        public string Title { get; set; }                 
        public string Description { get; set; }             
        public DateTime AssignedDate { get; set; }  
        public DateTime DueDate { get; set; }             
        public ICollection<LessonHomeworkLink> Links { get; set; }
        public ICollection<LessonUnitHomeworkSubmission> lessonUnitHomeworkSubmissions { get; set; }
    }
}
