using FluentValidation;

namespace EbuBridgeLmsSystem.Application.Features.LessonUnitAssignmentFeature.Commands.LessonUnitAssignmentFinish;

public sealed class LessonUnitAssignmentFinishCommandValidator : AbstractValidator<LessonUnitAssignmentFinishCommand>
{
    public LessonUnitAssignmentFinishCommandValidator()
    {
        RuleFor(s => s.LessonUnitAssignmentId)
            .NotEmpty()
            .WithMessage("Lesson Unit Assignment ID cannot be empty");
    }
}