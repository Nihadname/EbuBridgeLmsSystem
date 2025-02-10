using EbuBridgeLmsSystem.Domain.Entities;
using EbuBridgeLmsSystem.Domain.Repositories;
using LearningManagementSystem.DataAccess.Data.Implementations;

namespace EbuBridgeLmsSystem.Persistance.Data.Implementations
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        public CourseRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
