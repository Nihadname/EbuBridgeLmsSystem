using EbuBridgeLmsSystem.Domain.Entities;
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
