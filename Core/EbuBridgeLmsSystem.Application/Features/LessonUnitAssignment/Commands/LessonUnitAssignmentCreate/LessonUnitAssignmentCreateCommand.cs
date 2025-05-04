using LearningManagementSystem.Core.Entities.Common;
using MediatR;

namespace EbuBridgeLmsSystem.Application.Features.LessonUnitAssignment.Commands.LessonUnitAssignmentCreate
{
    public sealed record LessonUnitAssignmentCreateCommand:IRequest<Result<Unit>>
    {
        public Guid LessonUnitId { get; set; }
        public Guid StudentId { get; set; }
        public DateTime ScheduledStartTime { get; set; }
        public DateTime ScheduledEndTime { get; set; }
    }
}
