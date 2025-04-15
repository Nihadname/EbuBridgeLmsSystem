using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Features.LessonVideoFeature.Commands.LessonVideoFeatureCreate
{
    public sealed record LessonVideoCreateCommand:IRequest<Result<Unit>>
    {
        public string Title { get; init; }
        public FormFile File { get; init; }
        public Guid LessonUnitId { get; init; }
    }
}
