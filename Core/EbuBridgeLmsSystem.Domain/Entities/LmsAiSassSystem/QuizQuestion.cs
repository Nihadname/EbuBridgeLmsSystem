using LearningManagementSystem.Core.Entities.Common;

namespace EbuBridgeLmsSystem.Domain.Entities.LmsAiSassSystem
{
    public sealed class QuizQuestion:BaseEntity
    {
        public string Name { get; set; }
        public Guid QuizId { get; set; }
        public Quiz Quiz { get; set; }
        public ICollection<QuizQuestionOption> Options { get; set; }
    }
}
