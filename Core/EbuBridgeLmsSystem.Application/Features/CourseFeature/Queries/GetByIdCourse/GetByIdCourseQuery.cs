using EbuBridgeLmsSystem.Domain.Entities.Common;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Features.CourseFeature.Queries.GetByIdCourse
{
    public sealed record GetByIdCourseQuery:IRequest<Result<CourseReturnDto>>
    {
        public Guid Id { get; set; }
    }
}
