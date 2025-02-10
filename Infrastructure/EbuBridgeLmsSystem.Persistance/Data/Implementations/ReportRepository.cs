using EbuBridgeLmsSystem.Domain.Entities;
using EbuBridgeLmsSystem.Domain.Repositories;
using LearningManagementSystem.DataAccess.Data.Implementations;

namespace EbuBridgeLmsSystem.Persistance.Data.Implementations
{
    public class ReportRepository : Repository<Report>, IReportRepository
    {
        public ReportRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
