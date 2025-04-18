using FluentValidation;

namespace EbuBridgeLmsSystem.Application.Features.LessonUnitAssignment.Commands.LessonUnitAssignmentCreate
{
    public sealed class LessonUnitAssignmentCreateCommandValidator : AbstractValidator<LessonUnitAssignmentCreateCommand>
    {
        public LessonUnitAssignmentCreateCommandValidator()
        {
            RuleFor(s => s.StudentId)
             .NotEmpty().WithMessage("Student  request ID is required.")
             .Must(id => id != Guid.Empty).WithMessage("Student  request ID cannot be an empty GUID.");
            RuleFor(s => s.LessonUnitId)
             .NotEmpty().WithMessage("LessonUnitId request ID is required.")
             .Must(id => id != Guid.Empty).WithMessage("LessonUnitId request ID cannot be an empty GUID.");
            RuleFor(x => x.ScheduledStartTime)
              .NotNull()
        .GreaterThanOrEqualTo(DateTime.UtcNow)
        .WithMessage("StartDate must be in the future.");

            RuleFor(x => x.ScheduledEndTime)
                 .NotNull()
                .GreaterThanOrEqualTo(DateTime.UtcNow)
                .WithMessage("StartDate must be in the future.");
            RuleFor(x => x)
           .Must(x => x.ScheduledStartTime <= x.ScheduledEndTime);
        }
    }
}
