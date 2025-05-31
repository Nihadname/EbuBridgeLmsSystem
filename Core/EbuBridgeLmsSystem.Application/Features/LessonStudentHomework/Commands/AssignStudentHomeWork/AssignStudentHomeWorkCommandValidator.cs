using FluentValidation;

namespace EbuBridgeLmsSystem.Application.Features.LessonStudentHomework.Commands.AssignStudentHomeWork;

public sealed class AssignStudentHomeWorkCommandValidator: AbstractValidator<AssignStudentHomeWorkCommand>
{
    public AssignStudentHomeWorkCommandValidator()
    {
        RuleFor(x => x.LessonUnitId)
            .NotEmpty()
            .WithMessage("Lesson Unit ID is required.");
        RuleFor(x => x.StudentId)
            .NotEmpty()
            .WithMessage("Student ID is required.");

        // Rule for Title: Must not be empty and has a maximum length.
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Title is required.")
            .MaximumLength(200) // Example max length, adjust as needed
            .WithMessage("Title cannot exceed 200 characters.");

        // Rule for Description: Can be null or empty, but if provided, has a maximum length.
        RuleFor(x => x.Description)
            .MaximumLength(1000) // Example max length, adjust as needed
            .WithMessage("Description cannot exceed 1000 characters.")
            .When(x => !string.IsNullOrEmpty(x.Description)); // Apply rule only if description is not null or empty
        RuleFor(x => x.AssignedDate)
            .NotEmpty()
            .WithMessage("Assigned Date is required.")
            .Must(date => date != default(DateTime)) // Ensure it's not the default DateTime value
            .WithMessage("Assigned Date cannot be the default date.");

        // Rule for DueDate: Must not be a default (empty) date and must be after AssignedDate.
        RuleFor(x => x.DueDate)
            .NotEmpty()
            .WithMessage("Due Date is required.")
            .Must(date => date != default(DateTime)) // Ensure it's not the default DateTime value
            .WithMessage("Due Date cannot be the default date.")
            .GreaterThanOrEqualTo(x => x.AssignedDate)
            .WithMessage("Due Date must be on or after the Assigned Date.");

    }
}