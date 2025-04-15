using LearningManagementSystem.Core.Entities.Common;

namespace EbuBridgeLmsSystem.Domain.Entities
{
    public sealed class LessonUnitHomeworkSubmission:BaseEntity
    {
        public Guid LessonHomeworkId { get; set; }
        public LessonHomework LessonHomework { get; set; }
        public Guid StudentId { get; set; }
        public Student Student { get; set; }
        public DateTime SubmittedDate { get; set; }
        public string Content { get; set; }
        public decimal Grade { get; set; }
        public string Feedback { get; set; }
    }
}
