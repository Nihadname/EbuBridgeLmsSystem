using EbuBridgeLmsSystem.Application.Features.AddressFeature.Commands.Common;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;

namespace EbuBridgeLmsSystem.Application.Features.AddressFeature.Commands.UpdateAdress
{
    public sealed class UpdateAddressCommand: AddressBaseCommand, IRequest<Result<Unit>>
    {
        public Guid Id { get; set; }
    }
}
