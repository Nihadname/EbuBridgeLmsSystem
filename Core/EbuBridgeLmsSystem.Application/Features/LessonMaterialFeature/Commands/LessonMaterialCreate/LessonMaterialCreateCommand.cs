using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EbuBridgeLmsSystem.Application.Features.LessonMaterialFeature.Commands.LessonMaterialCreate
{
    public class LessonMaterialCreateCommand:IRequest<Result<Unit>>
    {
        public string Title { get; init; }
        public FormFile File { get; init; }
        public Guid LessonId { get; init; }
    }
}
