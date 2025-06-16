using LearningManagementSystem.Core.Entities.Common;

namespace EbuBridgeLmsSystem.Domain.Entities.LmsSystem
{
    public sealed class Language : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<CourseLanguage>  CourseLanguages { get; set; }
    }
}
