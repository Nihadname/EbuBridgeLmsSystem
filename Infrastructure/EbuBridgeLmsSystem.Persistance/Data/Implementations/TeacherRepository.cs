using EbuBridgeLmsSystem.Domain.Entities;
using LearningManagementSystem.Core.Repositories;
using LearningManagementSystem.DataAccess.Data.Implementations;

namespace EbuBridgeLmsSystem.Persistance.Data.Implementations
{
    public class TeacherRepository : Repository<Teacher>, ITeacherRepository
    {
        public TeacherRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
