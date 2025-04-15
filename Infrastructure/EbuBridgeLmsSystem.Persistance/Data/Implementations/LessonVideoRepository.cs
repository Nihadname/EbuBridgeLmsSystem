using EbuBridgeLmsSystem.Domain.Entities;
using EbuBridgeLmsSystem.Domain.Repositories;
using LearningManagementSystem.DataAccess.Data.Implementations;

namespace EbuBridgeLmsSystem.Persistance.Data.Implementations
{
    public class LessonVideoRepository : Repository<LessonUnitVideo>, ILessonUnitVideoRepository
    {
        public LessonVideoRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
