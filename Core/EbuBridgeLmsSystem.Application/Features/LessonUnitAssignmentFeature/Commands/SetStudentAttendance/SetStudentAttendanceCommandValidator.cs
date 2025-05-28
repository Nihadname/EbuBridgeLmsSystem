using FluentValidation;

namespace EbuBridgeLmsSystem.Application.Features.LessonUnitAssignmentFeature.Commands.SetStudentAttendance;

public sealed class SetStudentAttendanceCommandValidator : AbstractValidator<SetStudentAttendanceCommand>
{
    public SetStudentAttendanceCommandValidator()
    {
        RuleFor(x => x.StudentId)
            .NotEmpty()
            .WithMessage("Student ID is required");

        RuleFor(x => x.LessonUnitAssignmentId)
            .NotEmpty()
            .WithMessage("Lesson Unit Assignment ID is required");
        RuleFor(x => x.IsCompleted).NotNull().WithMessage("IsCompleted is required");
    }
}