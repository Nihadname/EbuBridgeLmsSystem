using EbuBridgeLmsSystem.Domain.Enums;
using LearningManagementSystem.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Domain.Entities
{
    public sealed class CourseStudentApprovalOutBox:BaseEntity
    {
        public Guid CourseStudentId { get; set; }
        public OutboxProccess OutboxProccess { get; set; }
        public DateTime? CompletedDate { get; set; }
    }
}
