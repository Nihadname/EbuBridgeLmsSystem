using FluentValidation;

namespace EbuBridgeLmsSystem.Application.Features.LessonFeature.Commands.LessonCreate
{
    public sealed class LessonCreateCommandValidator : AbstractValidator<LessonCreateCommand>
    {
        public LessonCreateCommandValidator()
        {
            RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100).WithMessage("Title must not exceed 100 characters.")
            .Must(title => !string.IsNullOrWhiteSpace(title)).WithMessage("Title cannot be just whitespace.");

            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Invalid LessonStatus. It must be one of the predefined enum values.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.")
                .Must(desc => !string.IsNullOrWhiteSpace(desc)).WithMessage("Description cannot be just whitespace.");

            RuleFor(x => x.LessonType)
                .IsInEnum().WithMessage("Invalid LessonType. It must be one of the predefined enum values.");

            RuleFor(x => x.GradingPolicy)
                .NotEmpty().WithMessage("Grading Policy is required.")
                .MaximumLength(200).WithMessage("Grading Policy must not exceed 200 characters.")
                .Must(gp => !string.IsNullOrWhiteSpace(gp)).WithMessage("Grading Policy cannot be just whitespace.");

            RuleFor(x => x.CourseId)
                .NotEmpty().WithMessage("CourseId is required.")
                .Must(id => id != Guid.Empty).WithMessage("CourseId must be a valid non-empty GUID.");

        }
    }
}
