﻿using LearningManagementSystem.Core.Entities.Common;

namespace EbuBridgeLmsSystem.Domain.Entities.LmsSystem
{
    public sealed class Course : BaseEntity
    {

        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DifficultyLevel DifficultyLevel { get; set; }
        public ICollection<Lesson> Lessons { get; set; }
        public int DurationInHours { get; set; }
        public string Requirements { get; set; }
        public decimal Price { get; set; }
        public ICollection<CourseStudent> CourseStudents { get; set; }
        public ICollection<CourseLanguage>  CourseLanguages { get; set; }
        public int MaxAmountOfPeople { get; set; }
        public ICollection<CourseTeacher> CourseTeachers { get; set; }

    }
    public enum DifficultyLevel
    {
        Beginner,
        MidLevel,
        Advanced
    }

}
