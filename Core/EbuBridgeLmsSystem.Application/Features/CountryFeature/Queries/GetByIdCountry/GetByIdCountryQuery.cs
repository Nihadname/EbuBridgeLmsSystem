using EbuBridgeLmsSystem.Application.Features.CountryFeature.CommanCommands;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;

namespace EbuBridgeLmsSystem.Application.Features.CountryFeature.Queries.GetByIdCountry
{
    public sealed record GetByIdCountryQuery:IRequest<Result<CountryReturnCommand>>
    {
        public Guid Id { get; init; }
    }
}
