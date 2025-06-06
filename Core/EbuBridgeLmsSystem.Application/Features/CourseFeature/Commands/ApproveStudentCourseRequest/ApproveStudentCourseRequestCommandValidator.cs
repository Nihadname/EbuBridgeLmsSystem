﻿using FluentValidation;

namespace EbuBridgeLmsSystem.Application.Features.CourseFeature.Commands.ApproveStudentCourseRequest
{
    public sealed class ApproveStudentCourseRequestCommandValidator : AbstractValidator<ApproveStudentCourseRequestCommand>
    {
        public ApproveStudentCourseRequestCommandValidator()
        {
            RuleFor(s => s.Id)
              .NotEmpty().WithMessage("Student course request ID is required.")
              .Must(id => id != Guid.Empty).WithMessage("Student course request ID cannot be an empty GUID.");


        }
    }
}
