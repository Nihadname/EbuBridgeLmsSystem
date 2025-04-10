﻿using LearningManagementSystem.Core.Entities.Common;

namespace EbuBridgeLmsSystem.Domain.Entities
{
    public sealed class Lesson:BaseEntity
    {
        public string Title { get; set; } 
        public DateTime ScheduledDate { get; set; }
        public TimeSpan Duration { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public LessonStatus Status { get; set; }
        public string Description { get; set; }
        public LessonType LessonType { get; set; }
        public string GradingPolicy { get; set; }
        public string MeetingLink { get; set; }
        public Guid CourseId { get; set; }
        public Course Course { get; set; }
        public Guid TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public ICollection<LessonStudent> lessonStudents { get; set; }  
        public ICollection<LessonVideo> lessonVideos { get; set; }
        public ICollection<LessonMaterial> lessonMaterials { get; set; }
        public ICollection<LessonQuiz>  lessonQuizzes { get; set; }
        public ICollection<LessonUnit> lessonUnits { get; set; }

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
