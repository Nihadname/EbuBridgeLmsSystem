using EbuBridgeLmsSystem.Domain.Entities.Common;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;

namespace EbuBridgeLmsSystem.Application.Features.CityFeature.Queries.GetAllCities
{
    public class GetAllCitiesQuery: IRequest<Result<PaginatedResult<CityListItemQuery>>>
    {
        public string Cursor { get; init; }
        public int Limit { get; init; }
        public string searchQuery { get; init; }
    }
}
