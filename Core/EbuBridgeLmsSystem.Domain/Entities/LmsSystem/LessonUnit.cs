using LearningManagementSystem.Core.Entities.Common;

namespace EbuBridgeLmsSystem.Domain.Entities.LmsSystem
{
    public sealed class LessonUnit : BaseEntity
    {
        public string Name { get; set; }
        public Guid LessonId { get; set; }
        public Lesson Lesson { get; set; }
        public ICollection<LessonUnitVideo> LessonUnitVideos { get; set; }
        public ICollection<LessonUnitMaterial> LessonUnitMaterials { get; set; }
        public ICollection<LessonUnitAssignment> lessonUnitAssignments { get; set; }
    }
}
