using LearningManagementSystem.Core.Entities.Common;

namespace EbuBridgeLmsSystem.Domain.Entities.LmsSystem
{
    public sealed class LessonUnitStudentHomeworkSubmission : BaseEntity
    {
        public Guid LessonUnitStudentHomeworkId { get; set; }
        public LessonUnitStudentHomework LessonUnitStudentHomework { get; set; }
        public DateTime SubmittedDate { get; set; }
        public string Content { get; set; }
        public decimal Grade { get; set; }
        public string Feedback { get; set; }
    }
}
