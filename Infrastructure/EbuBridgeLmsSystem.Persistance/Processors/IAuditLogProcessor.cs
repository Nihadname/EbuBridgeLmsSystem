using LearningManagementSystem.Core.Entities.Common;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Persistance.Processors
{
    public interface IAuditLogProcessor
    {
        Task HandleAuditLogs(IEnumerable<EntityEntry<BaseEntity>> entries);
    }
}
