using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Dtos.Country
{
    public record CountryCreateDto
    {
        public string Name { get; init; }
    }
}
