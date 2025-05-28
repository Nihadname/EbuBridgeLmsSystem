using EbuBridgeLmsSystem.Domain.Entities.LmsSystem;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;

namespace EbuBridgeLmsSystem.Application.Features.LessonUnitAssignmentFeature.Commands.SetStudentAttendance;

public record SetStudentAttendanceCommand():IRequest<Result<Unit>>
{
    public Guid StudentId { get; init; }    
    public Guid LessonUnitAssignmentId { get; init; }
    public bool IsCompleted { get; init; }
};