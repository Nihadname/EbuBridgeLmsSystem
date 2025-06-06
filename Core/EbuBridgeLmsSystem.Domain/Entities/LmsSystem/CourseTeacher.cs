﻿using LearningManagementSystem.Core.Entities.Common;

namespace EbuBridgeLmsSystem.Domain.Entities.LmsSystem
{
    public sealed class CourseTeacher : BaseEntity
    {
        public Guid CourseId { get; set; }
        public Course Course { get; set; }
        public Guid TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public ICollection<CourseTeacherLesson> CourseTeacherLessons { get; set; }
    }
}
