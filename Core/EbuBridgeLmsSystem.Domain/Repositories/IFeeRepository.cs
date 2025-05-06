using EbuBridgeLmsSystem.Domain.Entities.LmsSystem;
using System.Linq.Expressions;

namespace EbuBridgeLmsSystem.Domain.Repositories
{
    public interface IFeeRepository : IRepository<Fee>
    {
        Task<Fee> GetLaastFeeAsync(Expression<Func<Fee, bool>> predicate);
    }
}
