using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Features.CityFeature.Queries.GetByIdCity
{
    public sealed record GetByIdCityQuery:IRequest<Result<CityReturnQuery>>
    {
        public Guid Id { get; init; }
    }
}
