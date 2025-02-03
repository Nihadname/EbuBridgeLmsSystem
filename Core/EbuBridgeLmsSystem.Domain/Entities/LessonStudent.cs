using LearningManagementSystem.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningManagementSystem.Core.Entities
{
    public class LessonStudent:BaseEntity
    {
        public Guid LessonId  { get; set; }
        public Lesson Lesson { get; set; }
        public Guid StudentId { get; set; }
        public Student Student { get; set; }
        public bool Attended { get; set; }
        public ICollection<QuizResult> QuizResults { get; set; }

    }
}
