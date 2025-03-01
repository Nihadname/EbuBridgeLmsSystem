using LearningManagementSystem.Core.Entities.Common;
using MediatR;

namespace EbuBridgeLmsSystem.Application.Features.CityFeature.Commands.CreateCity
{
    public sealed record CreateCityCommand:IRequest<Result<Unit>>
    {
        public string Name { get; init; }
        public Guid CountryId { get; init; }
    }
}
