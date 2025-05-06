using EbuBridgeLmsSystem.Domain.Entities.LmsSystem;
using EbuBridgeLmsSystem.Domain.Repositories;
using LearningManagementSystem.DataAccess.Data.Implementations;

namespace EbuBridgeLmsSystem.Persistance.Data.Implementations
{
    public class RequstToRegisterRepository : Repository<RequestToRegister>, IRequstToRegisterRepository
    {
        public RequstToRegisterRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
