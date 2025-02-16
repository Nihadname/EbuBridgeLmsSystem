using LearningManagementSystem.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Domain.Entities
{
    public class Faculty:BaseEntity
    {
        public string Name { get; set; }
        public List<TeacherFacultyDegree> TeacherFacultyDegrees { get; set; }

    }
}
