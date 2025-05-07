using LearningManagementSystem.Core.Entities.Common;

namespace EbuBridgeLmsSystem.Domain.Entities.LmsAiSassSystem
{
    public sealed class QuizQuestionOption:BaseEntity
    {
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
        public Guid ArticleQuizQuestionId { get; set; }
        public ArticleQuizQuestion  ArticleQuizQuestion { get; set; }
    }
}
