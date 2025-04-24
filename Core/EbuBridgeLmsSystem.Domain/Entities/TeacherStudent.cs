using LearningManagementSystem.Core.Entities.Common;

namespace EbuBridgeLmsSystem.Domain.Entities
{
    public sealed class TeacherStudent:BaseEntity
    {
        public Guid TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public Guid StudentId { get; set; }
        public Student Student { get; set; }    
    }
}
