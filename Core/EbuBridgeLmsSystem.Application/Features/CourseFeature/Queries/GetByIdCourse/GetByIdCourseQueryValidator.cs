using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Features.CourseFeature.Queries.GetByIdCourse
{
    public sealed class GetByIdCourseQueryValidator : AbstractValidator<GetByIdCourseQuery>
    {
        public GetByIdCourseQueryValidator()
        {
            RuleFor(s => s.Id).Must(x => x != Guid.Empty).NotEmpty().NotNull();
        }
    }
}
