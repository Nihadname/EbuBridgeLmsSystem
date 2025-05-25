using EbuBridgeLmsSystem.Domain.Entities.LmsSystem;

namespace EbuBridgeLmsSystem.Domain.Repositories
{
    public interface ILessonUnitAssignmentRepository:IRepository<LessonUnitAssignment>
    {
        Task<LessonUnitAssignment> GetLatestUnitAssignment();
    }
}
