using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Features.LessonVideoFeature.Commands.LessonVideoFeatureCreate
{
    public sealed record LessonVideoFeatureCreateCommand
    {
        public string Title { get; init; }
        public FormFile File { get; init; }
        public Guid LessonId { get; init; }
    }
}
