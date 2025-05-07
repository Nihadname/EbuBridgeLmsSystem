using LearningManagementSystem.Core.Entities.Common;

namespace EbuBridgeLmsSystem.Domain.Entities.LmsSystem
{
    public sealed class QuizQuestion : BaseEntity
    {
        public string QuestionText { get; set; }
        public QuestionType QuestionType { get; set; }
        public string Explanation { get; set; }
        public DifficultyLevel Difficulty { get; set; }
        public ICollection<QuizOption> Options { get; set; }
        public string CorrectAnswer { get; set; }
        public Guid LessonQuizId { get; set; }
        public LessonQuiz LessonQuiz { get; set; }
    }
    public enum QuestionType
    {
        MultipleChoice,
        TrueFalse,
        ShortAnswer,
        Essay,
        FillInTheBlank,
        Matching
    }

  
}
