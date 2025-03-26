using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Features.CourseFeature.Commands.ApplyCourse
{
    public sealed record ApplyCourseCommand:IRequest<Result<Unit>>
    {
        public Guid StudentId { get; init; }
        public Guid CourseId { get; init; }
        public DateTime EnrolledDate { get; init; }
    }
}
