using Microsoft.AspNetCore.Http;

namespace EbuBridgeLmsSystem.Application.Dtos.LessonMaterial
{
    public sealed record LessonMaterialCreateDto
    {
        public string Title { get; set; }
        public FormFile File { get; set; }
        public Guid LessonId { get; set; }
    }
}
