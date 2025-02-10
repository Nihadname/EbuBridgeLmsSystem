using EbuBridgeLmsSystem.Domain.Entities;
using EbuBridgeLmsSystem.Domain.Repositories;
using LearningManagementSystem.DataAccess.Data.Implementations;

namespace EbuBridgeLmsSystem.Persistance.Data.Implementations
{
    public class ParentRepository : Repository<Parent>, IParentRepository
    {
        public ParentRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
