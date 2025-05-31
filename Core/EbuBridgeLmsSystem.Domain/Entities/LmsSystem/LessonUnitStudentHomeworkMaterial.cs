using LearningManagementSystem.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Domain.Entities.LmsSystem
{
    public class LessonUnitStudentHomeworkMaterial : BaseEntity
    {
        public string HomeWorkFileName { get; set; }
        public string FileName { get; set; }
        public Guid LessonUnitStudentHomeworkId { get; set; }
        public LessonUnitStudentHomework LessonUnitStudentHomework { get; set; }
    }
}
