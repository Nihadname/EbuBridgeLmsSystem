using EbuBridgeLmsSystem.Application.Dtos.Course;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Validators.CourseValidators
{
    public class CourseDeleteDtoValidator : AbstractValidator<CourseDeleteDto>
    {
        public CourseDeleteDtoValidator()
        {
            RuleFor(s => s.Id).Must(x => x != Guid.Empty).NotNull()
        .NotEmpty();
        }
    }
}
