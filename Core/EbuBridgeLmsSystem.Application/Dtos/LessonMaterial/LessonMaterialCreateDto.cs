using Microsoft.AspNetCore.Http;

namespace EbuBridgeLmsSystem.Application.Dtos.LessonMaterial
{
    public sealed record LessonMaterialCreateDto
    {
        public string Title { get; init; }
        public FormFile File { get; init; }
        public Guid LessonId { get; init; }
    }
}
