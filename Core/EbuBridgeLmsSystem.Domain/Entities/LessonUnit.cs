using LearningManagementSystem.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Domain.Entities
{
    public sealed class LessonUnit:BaseEntity
    {
        public string Name { get; set; }
        public DateTimeOffset LessonSetTime { get; set; }   
        public Guid LessonId { get; set; }
        public  Lesson Lesson { get; set; }
        public ICollection<LessonUnitVideo> LessonUnitVideos { get; set; }
        public ICollection<LessonUnitMaterial> LessonUnitMaterials { get; set; }
        public ICollection<LessonUnitAttendance> LessonUnitAttendances { get; set; }
        public ICollection<LessonUnitHomeworkSubmission> lessonUnitHomeworkSubmissions { get; set; }
    }
}
