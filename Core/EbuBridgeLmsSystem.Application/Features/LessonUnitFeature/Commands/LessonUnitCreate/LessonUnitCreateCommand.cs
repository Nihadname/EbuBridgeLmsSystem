using LearningManagementSystem.Core.Entities.Common;
using MediatR;

namespace EbuBridgeLmsSystem.Application.Features.LessonUnitFeature.Commands.LessonUnitCreate
{
    public sealed record LessonUnitCreateCommand:IRequest<Result<Unit>>
    {
        public string Name { get; set; }
        public DateTimeOffset LessonSetTime { get; set; }
        public Guid LessonId { get; set; }
    }
}
