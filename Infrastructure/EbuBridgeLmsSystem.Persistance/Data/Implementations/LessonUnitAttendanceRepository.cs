﻿using EbuBridgeLmsSystem.Domain.Entities;
using EbuBridgeLmsSystem.Domain.Repositories;
using LearningManagementSystem.DataAccess.Data.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Persistance.Data.Implementations
{
    public class LessonUnitAttendanceRepository : Repository<LessonUnitAttendance>, ILessonUnitAttendanceRepository
    {
        public LessonUnitAttendanceRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
