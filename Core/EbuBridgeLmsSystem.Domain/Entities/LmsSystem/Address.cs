using EbuBridgeLmsSystem.Domain.Entities.Common;
using LearningManagementSystem.Core.Entities.Common;

namespace EbuBridgeLmsSystem.Domain.Entities.LmsSystem
{
    public sealed class Address : BaseEntity
    {

        public string Region { get; set; }
        public string Street { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public Guid CityId { get; set; }
        public City City { get; set; }
        public Guid CountryId { get; set; }
        public Country Country { get; set; }

    }
}
