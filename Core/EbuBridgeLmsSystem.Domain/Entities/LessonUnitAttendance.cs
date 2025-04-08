using LearningManagementSystem.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Domain.Entities
{
    public sealed class LessonUnitAttendance:BaseEntity
    {
        public Guid LessonStudentId { get; set; }  
        public LessonStudent LessonStudent { get; set; }

        public Guid LessonUnitId { get; set; }  
        public LessonUnit LessonUnit { get; set; }

        public DateTime AttendanceDate { get; set; }
        public bool IsPresent { get; set; }
    }
}
