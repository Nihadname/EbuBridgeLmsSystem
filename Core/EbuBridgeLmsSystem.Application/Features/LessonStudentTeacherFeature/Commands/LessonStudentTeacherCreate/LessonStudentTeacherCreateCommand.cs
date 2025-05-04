using LearningManagementSystem.Core.Entities.Common;
using MediatR;

namespace EbuBridgeLmsSystem.Application.Features.LessonStudentFeature.Commands.LessonStudentCreate
{
    public sealed record LessonStudentTeacherCreateCommand : IRequest<Result<Unit>>
    {
        public Guid LessonId { get; init; }
        public Guid StudentId { get; init; }
    }
}
