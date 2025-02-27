using EbuBridgeLmsSystem.Domain.Entities;
using EbuBridgeLmsSystem.Persistance.Data;
using LearningManagementSystem.Core.Entities.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Security.Claims;
using System.Text.Json;
using System.Threading;

namespace EbuBridgeLmsSystem.Persistance.Processors
{
    public class AuditLogProcessor : IAuditLogProcessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _applicationDbContext;
        public AuditLogProcessor(IHttpContextAccessor httpContextAccessor, ApplicationDbContext applicationDbContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _applicationDbContext = applicationDbContext;
        }

        public async Task HandleAuditLogs(IEnumerable<EntityEntry<BaseEntity>> entries)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "System";
            var auditLogs = new List<AuditLog>();
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added || entry.State == EntityState.Modified || entry.State == EntityState.Deleted)
                {
                    var log = new AuditLog
                    {
                        TableName = entry.Entity.GetType().Name,
                        Action = entry.State.ToString(),
                        UserId = userId,
                        Changes = JsonSerializer.Serialize(entry.CurrentValues.ToObject())
                    };
                    auditLogs.Add(log);
                }
            }
            if (auditLogs.Any())
            {
                await _applicationDbContext.AuditLogs.AddRangeAsync(auditLogs);
            }
            
        }
    }
}
