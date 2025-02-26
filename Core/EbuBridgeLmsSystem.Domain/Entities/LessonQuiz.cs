using LearningManagementSystem.Core.Entities.Common;

namespace EbuBridgeLmsSystem.Domain.Entities
{
    public sealed class LessonQuiz:BaseEntity
    {
        public string Title { get; set; }
        public string Instructions { get; set; }
        public Guid? LessonId { get; set; }
        public Lesson Lesson { get; set; }
    }
}
