using EbuBridgeLmsSystem.Domain.Enums;
using System.Text.Json.Serialization;

namespace EbuBridgeLmsSystem.Application.Dtos.Teacher
{
    public sealed record TeacherCreateDto
    {
        public string Description { get; set; }
        public int experience { get; set; }
        public Position Position { get; set; }
        public decimal Salary { get; set; }
        public string FaceBookUrl { get; set; }
        public string pinterestUrl { get; set; }
        public string SkypeUrl { get; set; }
        public string IntaUrl { get; set; }
        [JsonIgnore]
        public string AppUserId { get; set; }
    }
}
