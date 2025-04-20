using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Features.LessonStudentFeature.LessonStudentApproval
{
    public sealed class LessonStudentApprovalCommandValidator : AbstractValidator<LessonStudentApprovalCommand>
    {
        public LessonStudentApprovalCommandValidator()
        {
            RuleFor(s => s.LessonStudentId)
              .NotEmpty().WithMessage("Student lesson request ID is required.")
              .Must(id => id != Guid.Empty).WithMessage("Student lesson request ID cannot be an empty GUID.");
        }
    }
}
