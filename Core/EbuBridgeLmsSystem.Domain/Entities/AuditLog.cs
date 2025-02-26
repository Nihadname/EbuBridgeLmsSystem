using LearningManagementSystem.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Domain.Entities
{
    public class AuditLog:BaseEntity
    {
        public string TableName { get; set; } 
        public string Action { get; set; } 
        public string UserId { get; set; } 
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string Changes { get; set; } 
    }
}
