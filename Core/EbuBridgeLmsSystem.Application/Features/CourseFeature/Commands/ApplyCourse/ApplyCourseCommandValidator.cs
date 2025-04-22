using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Features.CourseFeature.Commands.ApplyCourse
{
    public sealed class ApplyCourseCommandValidator : AbstractValidator<ApplyCourseCommand>
    {
        public ApplyCourseCommandValidator()
        {
            RuleFor(s => s.StudentId).Must(x => x != Guid.Empty).NotEmpty().NotNull();
            RuleFor(s => s.CourseId).Must(x => x != Guid.Empty).NotEmpty().NotNull();
        }
    }
}
