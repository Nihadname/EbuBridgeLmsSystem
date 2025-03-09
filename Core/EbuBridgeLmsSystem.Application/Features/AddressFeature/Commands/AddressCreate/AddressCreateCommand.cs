using EbuBridgeLmsSystem.Application.Features.AddressFeature.Commands.Common;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;

namespace EbuBridgeLmsSystem.Application.Features.AddressFeature.Commands.AddressCreate
{
    public sealed class AddressCreateCommand: AddressBaseCommand,IRequest<Result<Unit>>
    {
        public string AppUserId { get; set; }
    }
}
