using EbuBridgeLmsSystem.Application.Features.CountryFeature.CommanCommands;
using EbuBridgeLmsSystem.Domain.Entities.Common;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;

namespace EbuBridgeLmsSystem.Application.Features.CountryFeature.Queries.GetAllCountries
{
    public record GetAllCountriesQuery:IRequest<Result<PaginatedResult<CountryListItemCommand>>>
    {
        public string Cursor { get; init; }
        public int Limit { get; init; }
        public string searchQuery { get; init; }
    }
}
