using LearningManagementSystem.Core.Entities.Common;
using MediatR;

namespace EbuBridgeLmsSystem.Application.Features.AddressFeature.Commands.AddressCreate
{
    public sealed class AddressCreateCommand:IRequest<Result<Unit>>
    {
        public Guid CityId { get; set; }
        public  Guid CountryId { get; set; }    
        public string Region { get; set; }
        public string Street { get; set; }
        public string AppUserId { get; set; }
    }
}
