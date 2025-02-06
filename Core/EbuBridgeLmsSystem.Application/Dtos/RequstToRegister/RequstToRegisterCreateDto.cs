namespace EbuBridgeLmsSystem.Application.Dtos.RequstToRegister
{
    public sealed class RequstToRegisterCreateDto
    {
        public string FullName { get; set; }
        public int Age { get; set; }
        public bool IsParent { get; set; }
        public string PhoneNumber { get; set; }
        public Guid ChoosenCourse { get; set; }
        public string ChildName { get; set; }
        public int? ChildAge { get; set; }
        public string Email { get; set; }
        public string AiResponse { get; set; }
    }
}
