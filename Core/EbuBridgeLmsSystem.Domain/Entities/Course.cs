using LearningManagementSystem.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningManagementSystem.Core.Entities
{
    public class Course:BaseEntity
    {
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DifficultyLevel difficultyLevel { get; set; }
        public ICollection<Lesson> lessons { get; set; }
        public TimeSpan Duration { get; set; }
        public string Language { get; set; }
        public string Requirements { get; set; }
        public decimal Price { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public ICollection<CourseStudent> courseStudents { get; set; }
    }
    public enum DifficultyLevel
    {
        Beginner,
        MidLevel,
        Advanced
    }
}
