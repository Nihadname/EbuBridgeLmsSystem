using EbuBridgeLmsSystem.Application.Dtos.Course;
using FluentValidation;

namespace EbuBridgeLmsSystem.Application.Validators.CourseValidators
{
    public sealed class ApplyCourseDtoValidator:AbstractValidator<ApplyCourseDto>
    {
        public ApplyCourseDtoValidator()
        {
            RuleFor(s => s.StudentId).Must(x => x != Guid.Empty).NotEmpty().NotNull();
            RuleFor(s => s.CourseId).Must(x => x != Guid.Empty).NotEmpty().NotNull();
        }
    }
}
