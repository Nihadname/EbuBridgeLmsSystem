using LearningManagementSystem.Core.Entities.Common;

namespace EbuBridgeLmsSystem.Domain.Entities
{
    public class Note:BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
