using LearningManagementSystem.Core.Entities.Common;

namespace EbuBridgeLmsSystem.Domain.Entities.LmsAiSassSystem
{
    public sealed class ArticleQuizQuestion:BaseEntity
    {
        public string Text { get; set; }
        public string Explanation { get; set; }
        public Guid QuizId { get; set; }
        public Quiz Quiz { get; set; }
        public ICollection<QuizQuestionOption> Options { get; set; }
    }
}
