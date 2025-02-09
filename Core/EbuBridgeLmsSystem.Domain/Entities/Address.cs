using LearningManagementSystem.Core.Entities.Common;

namespace EbuBridgeLmsSystem.Domain.Entities
{
    public class Address:BaseEntity
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Street { get; set; }
        public Guid UserId { get; set; }
        public AppUser  User { get; set; }

    }
}
