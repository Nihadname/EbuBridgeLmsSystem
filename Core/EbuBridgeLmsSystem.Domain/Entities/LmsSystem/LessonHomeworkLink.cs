using LearningManagementSystem.Core.Entities.Common;

namespace EbuBridgeLmsSystem.Domain.Entities.LmsSystem
{
    public sealed class LessonHomeworkLink : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid LessonUnitStudentHomeworkId { get; set; }
        public LessonUnitStudentHomework LessonUnitStudentHomework { get; set; }
    }
}
