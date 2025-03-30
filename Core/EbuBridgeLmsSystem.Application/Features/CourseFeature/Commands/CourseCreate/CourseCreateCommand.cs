using EbuBridgeLmsSystem.Application.Dtos.Course;
using EbuBridgeLmsSystem.Domain.Entities;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EbuBridgeLmsSystem.Application.Features.CourseFeature.Commands.CourseCreate
{
    public sealed record CourseCreateCommand:IRequest<Result<Unit>>
    {
        public IFormFile formFile { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public DifficultyLevel difficultyLevel { get; init; }
        public int DurationHours { get; init; }
        public Guid LanguageId {  get; init; } 
        public string Requirements { get; init; }
        public decimal Price { get; init; }
        public int MaxAmountOfPeople { get; init; }
        public DateTime? StartDate { get; init; }
        public DateTime? EndDate { get; init; }
    }
}
