using LearningManagementSystem.Core.Entities.Common;

namespace EbuBridgeLmsSystem.Domain.Entities
{
    public sealed class Address:BaseEntity
    {
        public string Region { get; set; }
        public string Street { get; set; }
        public string AppUserId { get; set; }
        public AppUser  AppUser { get; set; }
        public Guid CityId { get; set; }
        public City City { get; set; }

    }
}
