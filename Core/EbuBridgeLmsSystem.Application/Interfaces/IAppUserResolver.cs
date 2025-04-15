using EbuBridgeLmsSystem.Domain.Entities;
using System.Linq.Expressions;

namespace EbuBridgeLmsSystem.Application.Interfaces
{
    public interface IAppUserResolver
    {
        string UserId { get; }
        string UserName { get; }
        bool IsAuthenticated { get; }

        Task<AppUser> GetCurrentUserAsync(Expression<Func<AppUser, bool>> predicate = null,params Func<IQueryable<AppUser>, IQueryable<AppUser>>[] includes);

    }
}
