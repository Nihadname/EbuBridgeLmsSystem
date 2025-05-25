using EbuBridgeLmsSystem.Domain.Entities.LmsSystem;
using EbuBridgeLmsSystem.Domain.Repositories;
using LearningManagementSystem.DataAccess.Data.Implementations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Persistance.Data.Implementations
{
    public class LessonUnitAssignmentRepository : Repository<LessonUnitAssignment>, ILessonUnitAssignmentRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public LessonUnitAssignmentRepository(ApplicationDbContext context) : base(context)
        {
            _applicationDbContext = context;
        }
        public async Task<LessonUnitAssignment> GetLatestUnitAssignment()
        {
            return await _applicationDbContext.lessonUnitAssignments.AsNoTracking().OrderByDescending(x => x.ScheduledEndTime).FirstOrDefaultAsync();
        }
    }
}
