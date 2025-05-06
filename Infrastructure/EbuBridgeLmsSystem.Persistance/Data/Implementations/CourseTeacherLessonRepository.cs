using EbuBridgeLmsSystem.Domain.Entities.LmsSystem;
using EbuBridgeLmsSystem.Domain.Repositories;
using LearningManagementSystem.DataAccess.Data.Implementations;

namespace EbuBridgeLmsSystem.Persistance.Data.Implementations
{
    public class CourseTeacherLessonRepository : Repository<CourseTeacherLesson>, ICourseTeacherLessonRepository
    {
        public CourseTeacherLessonRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
