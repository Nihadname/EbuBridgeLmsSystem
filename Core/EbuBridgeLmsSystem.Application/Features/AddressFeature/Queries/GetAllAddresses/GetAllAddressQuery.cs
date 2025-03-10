using EbuBridgeLmsSystem.Domain.Entities.Common;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;

namespace EbuBridgeLmsSystem.Application.Features.AddressFeature.Queries.GetAllAddresses
{
    public sealed record GetAllAddressQuery: IRequest<Result<PaginatedResult<AddressListItemQuery>>>
    {
        public string Cursor { get; init; }
        public int Limit { get; init; }
        public string searchQuery { get; init; }
    }
}
