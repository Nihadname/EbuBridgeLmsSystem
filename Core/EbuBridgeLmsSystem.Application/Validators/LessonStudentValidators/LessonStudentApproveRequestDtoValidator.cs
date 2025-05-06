using EbuBridgeLmsSystem.Domain.Entities.LmsSystem;
using FluentValidation;

namespace EbuBridgeLmsSystem.Application.Validators.LessonStudentValidators
{
    public sealed class LessonStudentApproveRequestDtoValidator : AbstractValidator<LessonStudentTeacher>
    {
        public LessonStudentApproveRequestDtoValidator()
        {
            RuleFor(s => s.Id).NotEmpty().NotNull().Must(x => x != Guid.Empty);
        }
    }
}
