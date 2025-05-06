using EbuBridgeLmsSystem.Domain.Entities.LmsSystem;
using EbuBridgeLmsSystem.Domain.Repositories;
using LearningManagementSystem.DataAccess.Data.Implementations;

namespace EbuBridgeLmsSystem.Persistance.Data.Implementations
{
    public class LessonStudentRepository : Repository<LessonStudentTeacher>, ILessonStudentRepository
    {
        public LessonStudentRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
