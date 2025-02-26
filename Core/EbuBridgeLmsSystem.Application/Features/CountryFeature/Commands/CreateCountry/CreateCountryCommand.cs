using LearningManagementSystem.Core.Entities.Common;
using MediatR;

namespace EbuBridgeLmsSystem.Application.Features.CountryFeature.Commands.CreateCountry
{
    public record CreateCountryCommand: IRequest<Result<Unit>>
    {
        public string Name { get; init; }    
    }
}
