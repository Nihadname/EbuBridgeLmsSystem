using System.Text.Json.Serialization;

namespace EbuBridgeLmsSystem.Application.Dtos.Report
{
    public class ReportCreateDto
    {
        public string Description { get; set; }
        [JsonIgnore]
        public string AppUserId { get; set; }
        public string ReportedUserId { get; set; }
        public Guid ReportOptionId { get; set; }
    }
}
