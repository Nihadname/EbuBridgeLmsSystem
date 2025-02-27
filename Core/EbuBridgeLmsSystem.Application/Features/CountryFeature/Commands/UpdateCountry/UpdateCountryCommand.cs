using LearningManagementSystem.Core.Entities.Common;
using MediatR;

namespace EbuBridgeLmsSystem.Application.Features.CountryFeature.Commands.UpdateCountry
{
    public record UpdateCountryCommand:IRequest<Result<Unit>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
