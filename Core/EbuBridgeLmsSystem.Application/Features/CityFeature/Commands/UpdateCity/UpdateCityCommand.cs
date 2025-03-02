using LearningManagementSystem.Core.Entities.Common;
using MediatR;

namespace EbuBridgeLmsSystem.Application.Features.CityFeature.Commands.UpdateCity
{
    public sealed record UpdateCityCommand:IRequest<Result<Unit>>
    {
        public Guid Id { get; set; }
        public string Name { get; init; }
        public Guid? CountryId { get; init; }
    }
}
