using EbuBridgeLmsSystem.Domain.Entities.LmsSystem;
using EbuBridgeLmsSystem.Domain.Repositories;
using LearningManagementSystem.DataAccess.Data.Implementations;

namespace EbuBridgeLmsSystem.Persistance.Data.Implementations;

public sealed class LessonUnitStudentHomeworkRepository:Repository<LessonUnitStudentHomework>, ILessonUnitStudentHomeworkRepository
{
    public LessonUnitStudentHomeworkRepository(ApplicationDbContext context) : base(context)
    {
    }
}