using EbuBridgeLmsSystem.Domain.Entities.Common;
using LearningManagementSystem.Core.Entities.Common;

namespace EbuBridgeLmsSystem.Domain.Entities.LmsAiSassSystem
{
    public sealed class AppUserQuizResult : BaseEntity
    {
        private static decimal FailPoint=60;
        public Guid AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public Guid QuizId { get; set; }
        public Quiz Quiz { get; set; }
        public decimal Grade { get; set; }
        public bool IsFailed { get; set; }
        public void UpdateFailingStatus()
        {
            if(this.Grade<= FailPoint)
            {
                this.IsFailed = true;
            }
            this.IsFailed = false;
        }
    }
}
