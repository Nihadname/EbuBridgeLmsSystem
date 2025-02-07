using LearningManagementSystem.Core.Entities.Common;

namespace EbuBridgeLmsSystem.Domain.Entities
{
    public class Parent:BaseEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public ICollection<Student> Students { get; set; }  
    }
}
