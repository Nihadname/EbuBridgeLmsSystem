﻿using EbuBridgeLmsSystem.Domain.Entities.LmsSystem;
using EbuBridgeLmsSystem.Domain.Repositories;
using LearningManagementSystem.DataAccess.Data.Implementations;

namespace EbuBridgeLmsSystem.Persistance.Data.Implementations
{
    public class CourseImageOutBoxRepository : Repository<CourseImageOutBox>, ICourseImageOutBoxRepository
    {
        public CourseImageOutBoxRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
