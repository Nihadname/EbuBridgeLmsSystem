using LearningManagementSystem.Core.Entities.Common;
using MediatR;

namespace EbuBridgeLmsSystem.Application.Features.AddressFeature.Commands.UpdateAdress
{
    public sealed class UpdateAddressCommand: IRequest<Result<Unit>>
    {
        public Guid Id { get; set; }
        public Guid? CityId { get; set; }
        public Guid? CountryId { get; set; }
        public string Region { get; set; }
        public string Street { get; set; }
    }
}
