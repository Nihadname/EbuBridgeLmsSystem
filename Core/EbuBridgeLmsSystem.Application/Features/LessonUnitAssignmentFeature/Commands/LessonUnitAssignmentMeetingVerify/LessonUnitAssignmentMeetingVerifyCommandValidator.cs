using FluentValidation;

namespace EbuBridgeLmsSystem.Application.Features.LessonUnitAssignmentFeature.Commands.LessonUnitAssignmentMeetingVerify;

public sealed class LessonUnitAssignmentMeetingVerifyCommandValidator:AbstractValidator<LessonUnitAssignmentMeetingVerifyCommand>
{
    public LessonUnitAssignmentMeetingVerifyCommandValidator()
    {
        RuleFor(s => s.LessonUnitAssignmentId).NotEmpty().NotNull().Must(x => x != Guid.Empty);
    }
}