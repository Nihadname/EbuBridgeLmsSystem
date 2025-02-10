using EbuBridgeLmsSystem.Domain.Entities;
using EbuBridgeLmsSystem.Domain.Repositories;
using LearningManagementSystem.DataAccess.Data.Implementations;


namespace EbuBridgeLmsSystem.Persistance.Data.Implementations
{
    public class ReportOptionRepository : Repository<ReportOption>, IReportOptionRepository
    {
        public ReportOptionRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
