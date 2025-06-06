﻿using EbuBridgeLmsSystem.Domain.Entities.LmsSystem;
using EbuBridgeLmsSystem.Domain.Repositories;
using LearningManagementSystem.DataAccess.Data.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Persistance.Data.Implementations
{
    public class CourseStudentRepository : Repository<CourseStudent>, ICourseStudentRepository
    {
        public CourseStudentRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
