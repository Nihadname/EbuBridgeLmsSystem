using LearningManagementSystem.Core.Entities.Common;

namespace EbuBridgeLmsSystem.Domain.Entities
{
    public sealed class CourseStudent:BaseEntity
    {
       
        public Guid CourseId { get; set; }
        public Course Course { get; set; }
        public Guid StudentId { get; set; }
        public Student Student { get; set; }
        public DateTimeOffset ApplyDate { get; set; }
        public DateTime? EnrolledDate { get; set; }
        public bool isApproved { get; set; }
    }
}
