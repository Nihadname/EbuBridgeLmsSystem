using FluentValidation;

namespace EbuBridgeLmsSystem.Application.Features.LessonStudentFeature.Commands.LessonStudentApproval
{
    public sealed class LessonStudentTeacherApprovalCommandValidator : AbstractValidator<LessonStudentTeacherApprovalCommand>
    {
        public LessonStudentTeacherApprovalCommandValidator()
        {
            RuleFor(s => s.LessonStudentId)
              .NotEmpty().WithMessage("Student lesson request ID is required.")
              .Must(id => id != Guid.Empty).WithMessage("Student lesson request ID cannot be an empty GUID.");
            RuleFor(s => s.TeacherId)
             .NotEmpty().WithMessage("Teacher request ID is required.")
             .Must(id => id != Guid.Empty).WithMessage("Teacher request ID cannot be an empty GUID.");
        }
    }
}
