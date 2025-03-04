using FluentValidation;

namespace EbuBridgeLmsSystem.Application.Features.CityFeature.Queries.GetAllCities
{
    public class GetAllCitiesQueryValidator:AbstractValidator<GetAllCitiesQuery>
    {
        public GetAllCitiesQueryValidator()
        {
            RuleFor(s => s.Limit).NotNull();
        }
    }
}
