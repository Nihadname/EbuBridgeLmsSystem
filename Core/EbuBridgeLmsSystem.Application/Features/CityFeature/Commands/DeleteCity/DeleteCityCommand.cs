using LearningManagementSystem.Core.Entities.Common;
using MediatR;

namespace EbuBridgeLmsSystem.Application.Features.CityFeature.Commands.DeleteCity
{
    public sealed record DeleteCityCommand:IRequest<Result<Unit>>
    {
        public Guid Id { get; init; }
    }
}
