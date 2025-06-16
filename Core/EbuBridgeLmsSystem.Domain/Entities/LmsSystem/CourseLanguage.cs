using LearningManagementSystem.Core.Entities.Common;

namespace EbuBridgeLmsSystem.Domain.Entities.LmsSystem;

public sealed class CourseLanguage:BaseEntity
{
    public Guid CourseId { get; set; }
    public Guid LanguageId { get; set; }
    
}