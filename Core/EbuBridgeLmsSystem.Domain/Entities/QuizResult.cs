using LearningManagementSystem.Core.Entities.Common;

namespace EbuBridgeLmsSystem.Domain.Entities
{
    public sealed class QuizResult:BaseEntity
    {
        public Guid QuizId { get; set; } 
        public LessonQuiz Quiz { get; set; } 
        public Guid StudentId { get; set; }
        public Student Student { get; set; }
        public decimal? Score { get; set; } 
        public DateTime AttemptedAt { get; set; } 
        public bool IsPassed { get; set; } 
    }
}
