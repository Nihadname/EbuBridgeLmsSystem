using LearningManagementSystem.Core.Entities.Common;

namespace EbuBridgeLmsSystem.Domain.Entities
{
    public sealed class TeacherFacultyDegree:BaseEntity
    {
        public Guid TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        public Guid FacultyId { get; set; }
        public Faculty Faculty { get; set; }

        public Guid DegreeId { get; set; }
        public Degree Degree { get; set; }
    }
}
