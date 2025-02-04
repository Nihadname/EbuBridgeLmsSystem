namespace EbuBridgeLmsSystem.Application.Dtos.Parent
{
    public class ParentCreateDto
    {
        public string AppUserId { get; set; }
        public List<Guid> StudentIds { get; set; }
    }
}
