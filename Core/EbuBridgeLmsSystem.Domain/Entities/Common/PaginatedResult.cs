using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Domain.Entities.Common
{
    public class PaginatedResult<T>
    {
        public List<T> Data { get; set; }
        public string NextCursor { get; set; }
    }
}
