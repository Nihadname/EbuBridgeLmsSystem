using EbuBridgeLmsSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Interfaces
{
    public interface IAppUserResolver
    {
        string UserId { get; }
        string UserName { get; }
        bool IsAuthenticated { get; }

        Task<AppUser> GetCurrentUserAsync(params Func<IQueryable<AppUser>, IQueryable<AppUser>>[] includes);

    }
}
