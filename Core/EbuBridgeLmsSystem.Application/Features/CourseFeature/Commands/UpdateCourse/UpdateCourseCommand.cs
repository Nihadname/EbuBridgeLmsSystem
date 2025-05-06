using EbuBridgeLmsSystem.Domain.Entities.LmsSystem;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;

using Microsoft.AspNetCore.Http;

namespace EbuBridgeLmsSystem.Application.Features.CourseFeature.Commands.UpdateCourse
{
    public sealed record UpdateCourseCommand:IRequest<Result<Unit>>
    {
        public Guid CourseId { get; init; }
        public IFormFile? formFile { get; init; } 
        public string Name { get; init; }
        public string Description { get; init; }
        public DifficultyLevel? difficultyLevel { get; init; }
        public int? DurationHours { get; init; }
        public Guid? LanguageId { get; init; }
        public string Requirements { get; init; }
        public decimal? Price { get; init; }
    }
}
