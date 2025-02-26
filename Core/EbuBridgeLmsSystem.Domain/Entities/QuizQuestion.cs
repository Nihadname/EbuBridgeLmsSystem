using LearningManagementSystem.Core.Entities.Common;

namespace EbuBridgeLmsSystem.Domain.Entities
{
    public sealed class QuizQuestion:BaseEntity
    {
        public string QuestionText { get; set; }
        public string QuestionType { get; set; } 
        public ICollection<QuizOption> Options { get; set; }
        public string CorrectAnswer { get; set; }
        public Guid LessonQuizId { get; set; }
        public LessonQuiz LessonQuiz { get; set; }
    }
}
