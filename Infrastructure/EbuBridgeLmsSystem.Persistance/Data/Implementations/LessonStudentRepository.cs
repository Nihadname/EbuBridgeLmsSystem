using EbuBridgeLmsSystem.Domain.Entities;
using EbuBridgeLmsSystem.Domain.Repositories;
using LearningManagementSystem.DataAccess.Data.Implementations;

namespace EbuBridgeLmsSystem.Persistance.Data.Implementations
{
    public class LessonStudentRepository : Repository<LessonStudent>, ILessonStudentRepository
    {
        public LessonStudentRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
