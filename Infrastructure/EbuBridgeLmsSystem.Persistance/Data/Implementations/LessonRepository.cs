using EbuBridgeLmsSystem.Domain.Entities;
using EbuBridgeLmsSystem.Domain.Repositories;
using LearningManagementSystem.DataAccess.Data.Implementations;

namespace EbuBridgeLmsSystem.Persistance.Data.Implementations
{
    public class LessonRepository : Repository<Lesson>, ILessonRepository
    {
        public LessonRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
