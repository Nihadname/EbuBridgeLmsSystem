using FluentValidation;

namespace EbuBridgeLmsSystem.Application.Features.LessonUnitFeature.Commands.LessonUnitCreate
{
    public sealed class LessonUnitCreateCommandValidator : AbstractValidator<LessonUnitCreateCommand>
    {
        public LessonUnitCreateCommandValidator()
        {
            RuleFor(s=>s.Name).MaximumLength(80).MinimumLength(5);
            RuleFor(s=>s.LessonSetTime).GreaterThanOrEqualTo(DateTime.Now)
            .WithMessage("Event date must be at least the current date and time.");
            RuleFor(s=>s.LessonId).Must(x => x != Guid.Empty).NotNull();
        }
    }
}
