using EbuBridgeLmsSystem.Domain.Entities.LmsSystem;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EbuBridgeLmsSystem.Application.Features.LessonStudentHomework.Commands.AssignStudentHomeWork;

public sealed record AssignStudentHomeWorkCommand:IRequest<Result<Unit>>
{
    public Guid LessonUnitId { get; init; }
    public Guid StudentId { get; init; } 
    public string Title { get; init; }
    public string Description { get; init; }
    public DateTime AssignedDate { get; init; }
    public DateTime DueDate { get; init; }
    public ICollection<HomeWorkLinkCreateDto> Links { get; init; }
    public ICollection<MaterialCreateDto> Materials { get; init; }
}

public sealed record MaterialCreateDto
{
    public string Name { get; init; }
    public IFormFile File { get; init; }
}

public sealed record HomeWorkLinkCreateDto()
{
    public string Name { get; init; }
}