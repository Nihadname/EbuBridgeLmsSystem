using EbuBridgeLmsSystem.Domain.Entities;
using EbuBridgeLmsSystem.Domain.Repositories;
using LearningManagementSystem.DataAccess.Data.Implementations;

namespace EbuBridgeLmsSystem.Persistance.Data.Implementations
{
    public class CourseTeacherRepository : Repository<CourseTeacher>, ICourseTeacherRepository
    {
        public CourseTeacherRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
