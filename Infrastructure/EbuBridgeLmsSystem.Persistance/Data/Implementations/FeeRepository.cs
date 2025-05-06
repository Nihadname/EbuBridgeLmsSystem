using EbuBridgeLmsSystem.Domain.Entities.LmsSystem;
using EbuBridgeLmsSystem.Domain.Repositories;
using LearningManagementSystem.DataAccess.Data.Implementations;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EbuBridgeLmsSystem.Persistance.Data.Implementations
{
    public class FeeRepository : Repository<Fee>, IFeeRepository
    {
        protected readonly ApplicationDbContext _context;
        public FeeRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Fee> GetLaastFeeAsync(Expression<Func<Fee, bool>> predicate)
        {
            return await _context.fees.OrderByDescending(f => f.DueDate).FirstOrDefaultAsync(predicate);
        }
    }
}
