using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Dtos.Course
{
    public sealed record CourseDeleteDto
    {
        public Guid Id { get; init; }
    }
}
