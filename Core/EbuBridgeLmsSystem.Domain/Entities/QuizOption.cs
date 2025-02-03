using LearningManagementSystem.Core.Entities.Common;

namespace EbuBridgeLmsSystem.Domain.Entities
{
    public class QuizOption:BaseEntity
    {
        public string Text { get; set; }
        public  bool IsCorrect { get; set; }
        public Guid QuizQuestionId { get; set; } 
        public QuizQuestion QuizQuestion { get; set; }
    }
}
