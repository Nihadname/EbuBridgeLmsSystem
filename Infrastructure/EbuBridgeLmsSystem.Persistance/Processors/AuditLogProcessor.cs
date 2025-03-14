using EbuBridgeLmsSystem.Application.Interfaces;
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
        private readonly IAppUserResolver _userResolver;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IHttpContextAccessor _contextAccessor;
        public AuditLogProcessor(ApplicationDbContext applicationDbContext, IAppUserResolver userResolver, IHttpContextAccessor contextAccessor)
        {

            _applicationDbContext = applicationDbContext;
            _userResolver = userResolver;
            _contextAccessor = contextAccessor;
        }

        public async Task HandleAuditLogs(IEnumerable<EntityEntry<BaseEntity>> entries)
        {
            var currentUserInSystem = await _userResolver.GetCurrentUserAsync();
            var userId = currentUserInSystem.Id?? "System";

            var auditLogs = new List<AuditLog>();
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added || entry.State == EntityState.Modified || entry.State == EntityState.Deleted)
                {
                    if (entry.Entity is AuditLog) continue;
                    var log = new AuditLog
                    {
                        TableName = entry.Entity.GetType().Name,
                        Action = entry.State.ToString(),
                        UserId = userId,
                        UserName=currentUserInSystem.UserName,
                        Changes = JsonSerializer.Serialize(entry.CurrentValues.ToObject()),
                        ClientIpAddress = GetClientIp()
                    };
                    auditLogs.Add(log);
                }
            }
            if (auditLogs.Any())
            {
                await _applicationDbContext.AuditLogs.AddRangeAsync(auditLogs);
            }
            
        }
        private string GetClientIp()
        {
            var context = _contextAccessor.HttpContext;
            if (context == null) return "Unknown";

            var forwardedFor = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (!string.IsNullOrEmpty(forwardedFor))
            {
                return forwardedFor.Split(',')[0]; 
            }

            return context.Connection.RemoteIpAddress?.ToString();
        }
    }
}
