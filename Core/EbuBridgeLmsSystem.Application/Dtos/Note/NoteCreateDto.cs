using System.Text.Json.Serialization;

namespace EbuBridgeLmsSystem.Application.Dtos.Note
{
    public sealed class NoteCreateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        [JsonIgnore]
        public string AppUserId { get; set; }
    }
}
