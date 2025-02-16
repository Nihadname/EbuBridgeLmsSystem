using LearningManagementSystem.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Domain.Entities
{
    public class TeacherFacultyDegree:BaseEntity
    {
        public Guid TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        public Guid FacultyId { get; set; }
        public Faculty Faculty { get; set; }

        public Guid DegreeId { get; set; }
        public Degree Degree { get; set; }
    }
}
