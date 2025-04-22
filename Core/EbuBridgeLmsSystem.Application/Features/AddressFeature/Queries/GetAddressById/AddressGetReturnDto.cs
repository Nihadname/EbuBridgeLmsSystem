using EbuBridgeLmsSystem.Application.Features.AddressFeature.Queries.GetAllAddresses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Features.AddressFeature.Queries.GetAddressById
{
    public sealed record AddressGetReturnDto
    {
        public Guid Id { get; init; }
        public string Country { get; init; }
        public string City { get; init; }
        public string Region { get; init; }
        public string Street { get; init; }
        public AppUserInAdress AppUserInAdress { get; init; }
    }
}
