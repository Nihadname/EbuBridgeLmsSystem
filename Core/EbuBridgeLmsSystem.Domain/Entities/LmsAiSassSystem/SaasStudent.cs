using EbuBridgeLmsSystem.Domain.Entities.Common;
using LearningManagementSystem.Core.Entities.Common;

namespace EbuBridgeLmsSystem.Domain.Entities.LmsAiSassSystem
{
    public sealed class SaasStudent:BaseEntity
    {
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public ICollection<SaasStudentSubscribtion> SaasStudentSubscribtions { get; set; }
    }
}
