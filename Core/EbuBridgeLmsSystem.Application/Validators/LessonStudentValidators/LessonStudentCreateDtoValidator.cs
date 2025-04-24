using EbuBridgeLmsSystem.Application.Dtos.LessonStudent;
using FluentValidation;

namespace EbuBridgeLmsSystem.Application.Validators.LessonStudentValidators
{
    public sealed class LessonStudentCreateDtoValidator : AbstractValidator<LessonStudentCreateDto>
    {
        public LessonStudentCreateDtoValidator()
        {
            RuleFor(s => s.LessonId).Must(x => x != Guid.Empty).NotEmpty().NotNull();
            RuleFor(s => s.StudentId).Must(x => x != Guid.Empty).NotEmpty().NotNull();
        }
    }
}
