using EbuBridgeLmsSystem.Domain.Entities;
using EbuBridgeLmsSystem.Domain.Repositories;
using LearningManagementSystem.DataAccess.Data.Implementations;

namespace EbuBridgeLmsSystem.Persistance.Data.Implementations
{
    public class LessonMaterialRepository : Repository<LessonMaterial>, ILessonMaterialRepository
    {
        public LessonMaterialRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
