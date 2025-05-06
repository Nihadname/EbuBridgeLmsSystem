using EbuBridgeLmsSystem.Domain.Enums;
using LearningManagementSystem.Core.Entities.Common;
using Microsoft.AspNetCore.Http;

namespace EbuBridgeLmsSystem.Domain.Entities.LmsSystem
{
    public sealed class CourseImageOutBox : BaseEntity
    {
        public Guid CourseId { get; set; }
        public OutboxProccess OutboxProccess { get; set; }
        public DateTime? CompletedDate { get; set; }
        public string TemporaryImagePath { get; set; }

    }
}
