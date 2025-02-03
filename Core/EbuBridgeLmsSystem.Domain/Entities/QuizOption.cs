using LearningManagementSystem.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningManagementSystem.Core.Entities
{
    public class QuizOption:BaseEntity
    {
        public string Text { get; set; }
        public  bool IsCorrect { get; set; }
        public Guid QuizQuestionId { get; set; } 
        public QuizQuestion QuizQuestion { get; set; }
    }
}
