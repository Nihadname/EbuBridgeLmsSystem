using FluentValidation;

namespace EbuBridgeLmsSystem.Application.Features.LessonUnitFeature.Commands.LessonUnitCreate
{
    public sealed class LessonUnitCreateCommandValidator : AbstractValidator<LessonUnitCreateCommand>
    {
        public LessonUnitCreateCommandValidator()
        {
        }
    }
}
