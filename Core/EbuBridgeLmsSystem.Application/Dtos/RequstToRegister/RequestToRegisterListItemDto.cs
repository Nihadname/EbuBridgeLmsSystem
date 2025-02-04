namespace EbuBridgeLmsSystem.Application.Dtos.RequstToRegister
{
    public record RequestToRegisterListItemDto
    {
        public string FullName { get; init; }
        public int Age { get; init; }
        public bool IsParent { get; init; }
        public string PhoneNumber { get; init; }
        public Guid ChoosenCourse { get; init; }
        public string ChildName { get; init; }
        public int? ChildAge { get; init; }
        public string Email { get; init; }
    }
}
