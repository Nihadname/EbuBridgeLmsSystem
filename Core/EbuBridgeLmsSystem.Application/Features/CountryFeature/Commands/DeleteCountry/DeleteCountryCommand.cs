using LearningManagementSystem.Core.Entities.Common;
using MediatR;

namespace EbuBridgeLmsSystem.Application.Features.CountryFeature.Commands.DeleteCountry
{
    public record DeleteCountryCommand:IRequest<Result<Unit>>
    {
        public Guid Id { get; set; }
    }
}
