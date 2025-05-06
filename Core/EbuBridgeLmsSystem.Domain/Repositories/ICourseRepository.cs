using EbuBridgeLmsSystem.Domain.DomainDtos.Course;
using EbuBridgeLmsSystem.Domain.Entities.LmsSystem;

namespace EbuBridgeLmsSystem.Domain.Repositories
{
    public interface ICourseRepository : IRepository<Course>
    {
        Task<CourseDomainReturnDto> GetCourseReturnDtoByIdAsync(Guid Id, CancellationToken cancellationToken);
    }
}
