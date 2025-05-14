using EbuBridgeLmsSystem.Domain.Entities.LmsAiSassSystem;
using EbuBridgeLmsSystem.Domain.Repositories;
using LearningManagementSystem.DataAccess.Data.Implementations;

namespace EbuBridgeLmsSystem.Persistance.Data.Implementations
{
    public class SaasStudentRepository : Repository<SaasStudent>, ISaasStudentRepository
    {
        public SaasStudentRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
