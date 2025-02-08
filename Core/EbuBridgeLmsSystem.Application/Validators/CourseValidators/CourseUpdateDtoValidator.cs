using EbuBridgeLmsSystem.Application.Dtos.Course;
using FluentValidation;

namespace EbuBridgeLmsSystem.Application.Validators.CourseValidators
{
    public class CourseUpdateDtoValidator : AbstractValidator<CourseUpdateDto>
    {
        public CourseUpdateDtoValidator()
        {
            RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(160)
            .When(x => x.Name != null);

            RuleFor(x => x.Description)
         .NotEmpty()
         .MinimumLength(4)
         .MaximumLength(250)
         .When(x => x.Description != null);

        }
    }
}
