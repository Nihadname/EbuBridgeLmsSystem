using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Dtos.Address
{
    public sealed record DeleteAddressDto
    {
        public Guid Id  { get; set; }
    }
}
