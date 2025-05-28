using LearningManagementSystem.Core.Entities.Common;
using MediatR;

namespace EbuBridgeLmsSystem.Application.Features.LessonUnitAssignmentFeature.Commands.LessonUnitAssignmentMeetingVerify;

public record LessonUnitAssignmentMeetingVerifyCommand:IRequest<Result<Unit>>
{
    public Guid LessonUnitAssignmentId { get; init; } 
};