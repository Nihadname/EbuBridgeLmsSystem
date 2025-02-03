using LearningManagementSystem.Core.Entities.Common;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningManagementSystem.Core.Entities
{
    public class QuizQuestion:BaseEntity
    {
        public string QuestionText { get; set; }
        public string QuestionType { get; set; } 
        public ICollection<QuizOption> Options { get; set; }
        public string CorrectAnswer { get; set; }
        public Guid LessonQuizId { get; set; }
        public LessonQuiz LessonQuiz { get; set; }
    }
}
