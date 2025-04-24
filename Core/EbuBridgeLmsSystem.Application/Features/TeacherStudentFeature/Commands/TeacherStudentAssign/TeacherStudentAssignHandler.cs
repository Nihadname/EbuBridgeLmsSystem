using LearningManagementSystem.Core.Entities.Common;
using MediatR;

namespace EbuBridgeLmsSystem.Application.Features.TeacherStudentFeature.Commands.TeacherStudentAssign
{
    public sealed class TeacherStudentAssignHandler : IRequestHandler<TeacherStudentAssignCommand, Result<Unit>>
    {
        public Task<Result<Unit>> Handle(TeacherStudentAssignCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
