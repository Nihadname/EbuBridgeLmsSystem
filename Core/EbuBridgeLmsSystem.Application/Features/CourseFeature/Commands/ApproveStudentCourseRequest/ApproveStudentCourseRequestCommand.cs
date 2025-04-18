using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Features.CourseFeature.Commands.ApproveStudentCourseRequest
{
    public sealed record ApproveStudentCourseRequestCommand:IRequest<Result<Unit>>
    {
        public Guid Id { get; init; }

    }
}
