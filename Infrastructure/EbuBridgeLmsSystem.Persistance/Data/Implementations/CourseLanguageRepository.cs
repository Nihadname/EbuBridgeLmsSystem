using EbuBridgeLmsSystem.Domain.Entities.LmsSystem;
using EbuBridgeLmsSystem.Domain.Repositories;
using LearningManagementSystem.DataAccess.Data.Implementations;

namespace EbuBridgeLmsSystem.Persistance.Data.Implementations;

public class CourseLanguageRepository: Repository<CourseLanguage>, ICourseLanguageRepository
{
    public CourseLanguageRepository(ApplicationDbContext context) : base(context)
    {
    }
}