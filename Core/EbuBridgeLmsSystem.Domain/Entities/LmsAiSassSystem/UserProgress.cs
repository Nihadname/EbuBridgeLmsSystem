using EbuBridgeLmsSystem.Domain.Entities.Common;
using LearningManagementSystem.Core.Entities.Common;

namespace EbuBridgeLmsSystem.Domain.Entities.LmsAiSassSystem
{
    public sealed class UserProgress:BaseEntity
    {
        public Guid AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public Guid ArticleId { get; set; }
        public Article Article { get; set; }
        public bool IsFinisHed {  get; set; }
        public DateTimeOffset CompeletedDate { get; set; }

    }
}
