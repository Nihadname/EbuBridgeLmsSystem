using LearningManagementSystem.Core.Entities.Common;

namespace EbuBridgeLmsSystem.Domain.Entities
{
    public sealed class City:BaseEntity
    {
        
        public string Name { get; set; }
        public Guid CountryId { get; set; }
        public Country Country { get; set; }
        public ICollection<Address> Addresses { get; set; }
    }
}
