using FluentValidation;

namespace EbuBridgeLmsSystem.Application.Features.TeacherStudentFeature.Commands.TeacherStudentAssign
{
    public sealed class TeacherStudentAssignCommandValidator : AbstractValidator<TeacherStudentAssignCommand>
    {
        public TeacherStudentAssignCommandValidator()
        {
            RuleFor(x => x.TeacherId).NotEmpty().Must(x => x != Guid.Empty).NotNull();
            RuleFor(x => x.StudentId).NotEmpty().Must(x => x != Guid.Empty).NotNull();
        }
    }
}
