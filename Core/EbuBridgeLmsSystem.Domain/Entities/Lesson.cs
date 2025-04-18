﻿using LearningManagementSystem.Core.Entities.Common;

namespace EbuBridgeLmsSystem.Domain.Entities
{
    public sealed class Lesson:BaseEntity
    {
        public string Title { get; set; } 
        public LessonStatus Status { get; set; }
        public string Description { get; set; }
        public LessonType LessonType { get; set; }
        public string GradingPolicy { get; set; }
        public Guid CourseId { get; set; }
        public Course Course { get; set; }
        public Guid TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public ICollection<LessonStudent> LessonStudents { get; set; }  
        public ICollection<LessonQuiz>  LessonQuizzes { get; set; }
        public ICollection<LessonUnit> LessonUnits { get; set; }

    }
    public enum LessonStatus
    {
        Scheduled,
        Completed,
        Canceled
    }
    public enum LessonType
    {
        Lecture,
        Lab,
        Tutorial,
        Online
    }
}
