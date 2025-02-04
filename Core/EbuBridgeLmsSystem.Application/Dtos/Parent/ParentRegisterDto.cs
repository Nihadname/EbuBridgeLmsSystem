using EbuBridgeLmsSystem.Application.Dtos.Auth;

namespace EbuBridgeLmsSystem.Application.Dtos.Parent
{
    public class ParentRegisterDto
    {
        public RegisterDto Register { get; set; }
        public ParentCreateDto Parent { get; set; }
    }
}
