using LearningManagementSystem.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Domain.Entities.LmsAiSassSystem
{
    public sealed class SaasStudentSubscribtion:BaseEntity
    {
        public Guid SaasStudentId { get; set; }
        public SaasStudent SaasStudent { get; set; }
        public Guid SubscribtionId { get; set; }
        public Subscribtion Subscribtion { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsTrialAvailable { get; set; }
        public int TrialDays { get; set; }
    }
}
