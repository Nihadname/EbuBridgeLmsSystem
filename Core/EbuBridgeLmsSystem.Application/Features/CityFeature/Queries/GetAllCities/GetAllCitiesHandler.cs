using EbuBridgeLmsSystem.Domain.Entities.Common;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Features.CityFeature.Queries.GetAllCities
{
    public class GetAllCitiesHandler : IRequestHandler<GetAllCitiesQuery, Result<PaginatedResult<CityListItemQuery>>>
    {
        public Task<Result<PaginatedResult<CityListItemQuery>>> Handle(GetAllCitiesQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
