using LearningManagementSystem.Core.Entities.Common;
using MediatR;

namespace EbuBridgeLmsSystem.Application.Features.AddressFeature.Commands.DeleteAddress
{
    public sealed record DeleteAddressCommand:IRequest<Result<Unit>>
    {
        public Guid Id { get; set; }
    }
}
