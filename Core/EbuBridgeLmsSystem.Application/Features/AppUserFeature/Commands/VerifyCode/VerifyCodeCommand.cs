using LearningManagementSystem.Core.Entities.Common;
using MediatR;

namespace EbuBridgeLmsSystem.Application.Features.AppUserFeature.Commands.VerifyCode
{
    public record VerifyCodeCommand:IRequest<Result<string>>
    {
        public string Email { get; set; }
        public string Code { get; set; }
    }
}
