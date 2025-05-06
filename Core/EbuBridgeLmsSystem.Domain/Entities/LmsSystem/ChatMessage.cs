using EbuBridgeLmsSystem.Domain.Entities.Common;
using LearningManagementSystem.Core.Entities.Common;

namespace EbuBridgeLmsSystem.Domain.Entities.LmsSystem
{
    public sealed class ChatMessage : BaseEntity
    {
        public string SenderAppUserId { get; set; }
        public string ReceiverAppUserId { get; set; }
        public AppUser SenderAppUser { get; set; }
        public AppUser ReceiverAppUser { get; set; }
        public string Message { get; set; }
        public DateTime SentAt { get; set; }
    }
}
