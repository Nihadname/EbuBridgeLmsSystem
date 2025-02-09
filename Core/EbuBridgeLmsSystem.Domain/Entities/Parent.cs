using LearningManagementSystem.Core.Entities.Common;

namespace EbuBridgeLmsSystem.Domain.Entities
{
    public class Parent:BaseEntity
    {
        public Guid UserId { get; set; }
        public AppUser User { get; set; }
        public ICollection<Student> Students { get; set; }  
    }
}
