using LearningManagementSystem.Core.Entities.Common;

namespace EbuBridgeLmsSystem.Domain.Entities
{
    public sealed class Degree:BaseEntity
    {
       
        public string Name { get; set; }
        public string Level { get; set; }
        public List<TeacherFacultyDegree> TeacherFacultyDegrees { get; set; }


    }
}
