using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Features.CourseFeature.Queries.GetAllCourse.GetAllCourseQuery
{
    internal class GetAllCourseRequestQueryValidator : AbstractValidator<GetAllCourseRequestQuery>
    {
        public GetAllCourseRequestQueryValidator()
        {
            RuleFor(s => s.Limit).NotNull();
        }
    }
}
