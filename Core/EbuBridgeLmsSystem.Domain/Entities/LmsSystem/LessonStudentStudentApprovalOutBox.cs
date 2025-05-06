using EbuBridgeLmsSystem.Domain.Entities.ValueObjects;
using EbuBridgeLmsSystem.Domain.Enums;
using LearningManagementSystem.Core.Entities.Common;

namespace EbuBridgeLmsSystem.Domain.Entities.LmsSystem
{
    public sealed class LessonStudentStudentApprovalOutBox : BaseEntity
    {
        public Guid LessonStudentId { get; set; }
        public OutboxProccess OutboxProccess { get; set; }
        public DateTime? CompletedDate { get; set; }
        public TeacherDetailApprovalOutBox TeacherDetailApprovalOutBox { get; set; }
    }
}
