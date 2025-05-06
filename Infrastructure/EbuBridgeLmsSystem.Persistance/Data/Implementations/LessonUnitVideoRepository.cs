using EbuBridgeLmsSystem.Domain.Entities.LmsSystem;
using EbuBridgeLmsSystem.Domain.Repositories;
using LearningManagementSystem.DataAccess.Data.Implementations;

namespace EbuBridgeLmsSystem.Persistance.Data.Implementations
{
    public class LessonUnitVideoRepository : Repository<LessonUnitVideo>, ILessonUnitVideoRepository
    {
        public LessonUnitVideoRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
