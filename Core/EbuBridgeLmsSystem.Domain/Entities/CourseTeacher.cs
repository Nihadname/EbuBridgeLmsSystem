using LearningManagementSystem.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Domain.Entities
{
    public sealed class CourseTeacher:BaseEntity
    {
        public Guid CourseId { get; set; }
        public Course Course { get; set; }
        public Guid TeacherId { get; set; }
        public Teacher Teacher { get; set; }
    }
}
