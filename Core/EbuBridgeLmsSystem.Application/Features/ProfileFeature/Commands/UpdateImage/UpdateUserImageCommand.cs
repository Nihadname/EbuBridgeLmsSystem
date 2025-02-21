using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EbuBridgeLmsSystem.Application.Features.ProfileFeature.Commands.UpdateImage
{
    public record UpdateUserImageCommand:IRequest<Result<Unit>>
    {
        public IFormFile FormFile { get; init ; }
    }
}
