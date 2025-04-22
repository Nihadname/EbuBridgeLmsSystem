using LearningManagementSystem.Core.Entities.Common;
using MediatR;

namespace EbuBridgeLmsSystem.Application.Features.AddressFeature.Queries.GetAddressById
{
    public sealed record GetAddressByIdQuery:IRequest<Result<AddressGetReturnDto>>
    {
        public Guid Id { get; init; }
    }
}
