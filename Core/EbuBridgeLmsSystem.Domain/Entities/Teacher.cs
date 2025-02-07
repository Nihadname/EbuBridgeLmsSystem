using LearningManagementSystem.Core.Entities.Common;

namespace EbuBridgeLmsSystem.Domain.Entities
{
    public class Teacher:BaseEntity
    {
        public string Description { get; set; }
        public string degree { get; set; }
        public int experience { get; set; }
        public string faculty { get; set; }
        public string Position { get; set; }
        public decimal Salary { get; set; }
        public string FaceBookUrl { get; set; }
        public string pinterestUrl { get; set; }
        public string SkypeUrl { get; set; }
        public string IntaUrl { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
