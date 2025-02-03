using LearningManagementSystem.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningManagementSystem.Core.Entities
{
    public class QuizResult:BaseEntity
    {
        public Guid QuizId { get; set; } 
        public LessonQuiz Quiz { get; set; } 
        public Guid StudentId { get; set; }
        public Student Student { get; set; }
        public decimal? Score { get; set; } 
        public DateTime AttemptedAt { get; set; } 
        public bool IsPassed { get; set; } 
    }
}
