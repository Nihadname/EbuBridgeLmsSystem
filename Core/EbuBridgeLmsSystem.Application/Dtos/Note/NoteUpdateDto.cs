namespace EbuBridgeLmsSystem.Application.Dtos.Note
{
    public record NoteUpdateDto
    {
        public string Title { get; init; }
        public string Description { get; init; }
        public string CategoryName { get; init; }
    }
}
