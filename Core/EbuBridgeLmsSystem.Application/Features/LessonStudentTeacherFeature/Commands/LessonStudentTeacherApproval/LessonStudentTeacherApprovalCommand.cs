using LearningManagementSystem.Core.Entities.Common;
using MediatR;

namespace EbuBridgeLmsSystem.Application.Features.LessonStudentFeature.Commands.LessonStudentApproval
{
    public sealed record LessonStudentTeacherApprovalCommand : IRequest<Result<Unit>>
    {
        public Guid LessonStudentId { get; init; }
        public Guid TeacherId { get; init; }
    }
}
