using EbuBridgeLmsSystem.Domain.Entities.LmsSystem;
using EbuBridgeLmsSystem.Domain.Repositories;
using LearningManagementSystem.DataAccess.Data.Implementations;

namespace EbuBridgeLmsSystem.Persistance.Data.Implementations
{
    public class CourseStudentApprovalOutBoxRepository : Repository<CourseStudentApprovalOutBox>, ICourseStudentApprovalOutBoxRepository
    {
        public CourseStudentApprovalOutBoxRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
