namespace EbuBridgeLmsSystem.Application.Dtos.Note
{
    public record NoteListItemDto
    {
        public Guid Id { get; init; }
        public string Title { get; init; }
        public string Description { get; init; }
        public string CategoryName { get; init; }
    }
}
