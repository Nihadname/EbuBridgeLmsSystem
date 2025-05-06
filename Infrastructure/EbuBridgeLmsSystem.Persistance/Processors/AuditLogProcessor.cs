using EbuBridgeLmsSystem.Domain.Entities.Common;
using EbuBridgeLmsSystem.Domain.Entities.LmsSystem;
using EbuBridgeLmsSystem.Persistance.Data;
using LearningManagementSystem.Core.Entities.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Security.Claims;
using System.Text.Json;

namespace EbuBridgeLmsSystem.Persistance.Processors
{
    public class AuditLogProcessor : IAuditLogProcessor
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<AppUser> _userManager;
        public AuditLogProcessor(ApplicationDbContext applicationDbContext, IHttpContextAccessor contextAccessor, UserManager<AppUser> userManager)
        {

            _applicationDbContext = applicationDbContext;
            _contextAccessor = contextAccessor;
            _userManager = userManager;
        }

        public async Task HandleAuditLogs(IEnumerable<EntityEntry<BaseEntity>> entries)
        {
            var userId = _contextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "System";
            var userName = _contextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value is not null ? (await _userManager.FindByIdAsync(userId)).UserName : "System";
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
                        UserName= userName,
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
