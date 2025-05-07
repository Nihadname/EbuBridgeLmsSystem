using LearningManagementSystem.Core.Entities.Common;

namespace EbuBridgeLmsSystem.Domain.Entities.LmsAiSassSystem
{
    public sealed class Quiz:BaseEntity
    {
        public string Title { get; set; }
        public Guid ArticleId { get; set; }
        public Article Article { get; set; }
        public ICollection<ArticleQuizQuestion> QuizQuestions { get; set; }
    }
}
