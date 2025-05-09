using EbuBridgeLmsSystem.Domain.Entities.Common;
using LearningManagementSystem.Core.Entities.Common;

namespace EbuBridgeLmsSystem.Domain.Entities.LmsAiSassSystem
{
    public sealed class Subscribtion:BaseEntity
    {
        public SubscritionType SubscritionType { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public ICollection<SaasStudentSubscribtion> SaasStudentSubscribtions { get; set; }
    }
    public enum SubscritionType
    {
        Basic,
        Mid,
        Premium
    }
}
