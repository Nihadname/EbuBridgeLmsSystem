using LearningManagementSystem.Core.Entities.Common;
using MediatR;

namespace EbuBridgeLmsSystem.Application.Features.LessonUnitAssignmentFeature.Commands.LessonUnitAssignmentFinish;

public record LessonUnitAssignmentFinishCommand():IRequest<Result<Unit>>
{
    public Guid LessonUnitAssignmentId { get; init; }
};