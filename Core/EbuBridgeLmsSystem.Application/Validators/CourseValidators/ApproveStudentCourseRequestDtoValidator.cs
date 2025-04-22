using EbuBridgeLmsSystem.Application.Dtos.Course;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Validators.CourseValidators
{
    public sealed class ApproveStudentCourseRequestDtoValidator : AbstractValidator<ApproveStudentCourseRequestDto>
    {
        public ApproveStudentCourseRequestDtoValidator()
        {
            RuleFor(s => s.Id).NotEmpty().NotNull().Must(x => x != Guid.Empty);
        }
    }
}
