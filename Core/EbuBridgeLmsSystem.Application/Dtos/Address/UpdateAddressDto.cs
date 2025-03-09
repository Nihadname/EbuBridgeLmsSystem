using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Dtos.Address
{
    public class UpdateAddressDto
    {
        public Guid Id { get; set; }
        public Guid? CityId { get; set; }
        public Guid? CountryId { get; set; }
        public string Region { get; set; }
        public string Street { get; set; }
    }
}
