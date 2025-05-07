using LearningManagementSystem.Core.Entities.Common;

namespace EbuBridgeLmsSystem.Domain.Entities.LmsSystem
{
    public sealed class QuizResult : BaseEntity
    {
        private static decimal FailingPoint=60;
        public Guid QuizId { get; set; }
        public LessonQuiz Quiz { get; set; }
        public Guid StudentId { get; set; }
        public Student Student { get; set; }
        public decimal? Score { get; set; }
        public DateTime AttemptedAt { get; set; }
        public bool IsPassed { get; set; }
        public void SetFailPoint()
        {
            if (this.Score <= FailingPoint)
            {
                this.IsPassed = false;
            }
            this.IsPassed=true;
        }
    }
}
