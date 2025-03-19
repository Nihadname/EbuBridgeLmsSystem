using LearningManagementSystem.Core.Entities.Common;
using MediatR;

namespace EbuBridgeLmsSystem.Application.Features.CourseFeature.Commands.DeleteCourse
{
    public sealed record DeleteCourseCommand:IRequest<Result<Unit>>
    {
        public Guid Id { get; init; }
    }
}
