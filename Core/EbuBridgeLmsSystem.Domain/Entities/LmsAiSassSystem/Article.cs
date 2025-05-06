using LearningManagementSystem.Core.Entities.Common;

namespace EbuBridgeLmsSystem.Domain.Entities.LmsAiSassSystem
{
    public sealed class Article:BaseEntity
    {
        public string Title { get; set; }  
        public string Content { get; set; }  
        public string Topic { get; set; }
        public ICollection<Quiz> Quizzes { get; set; }
    }
}
