using EbuBridgeLmsSystem.Domain.Entities;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Features.LessonFeature.Commands.LessonCreate
{
    public sealed record LessonCreateCommand:IRequest<Result<Unit>>
    {
        public string Title { get; init; }
        public LessonStatus Status { get; init; }
        public string Description { get; init; }
        public LessonType LessonType { get; init; }
        public string GradingPolicy { get; init; }
        public Guid CourseId { get; init; }
    }
}
