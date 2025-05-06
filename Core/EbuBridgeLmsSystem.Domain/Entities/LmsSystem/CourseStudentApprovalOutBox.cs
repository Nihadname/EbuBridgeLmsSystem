using EbuBridgeLmsSystem.Domain.Entities.ValueObjects;
using EbuBridgeLmsSystem.Domain.Enums;
using LearningManagementSystem.Core.Entities.Common;

namespace EbuBridgeLmsSystem.Domain.Entities.LmsSystem
{
    public sealed class CourseStudentApprovalOutBox : BaseEntity
    {
        public Guid CourseStudentId { get; set; }
        public OutboxProccess OutboxProccess { get; set; }
        public DateTime? CompletedDate { get; set; }
    }
}
