using EbuBridgeLmsSystem.Domain.Entities.LmsSystem;
using EbuBridgeLmsSystem.Domain.Repositories;
using LearningManagementSystem.DataAccess.Data.Implementations;

namespace EbuBridgeLmsSystem.Persistance.Data.Implementations
{
    public class LessonUnitMaterialRepository : Repository<LessonUnitMaterial>, ILessonUnitMaterialRepository
    {
        public LessonUnitMaterialRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
