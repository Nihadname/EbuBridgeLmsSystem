using FluentValidation;

namespace EbuBridgeLmsSystem.Application.Features.CountryFeature.Queries.GetAllCountries
{
    public class GetAllCountriesQueryValidator : AbstractValidator<GetAllCountriesQuery>
    {
        public GetAllCountriesQueryValidator()
        {
            RuleFor(s => s.Limit).NotNull();
        }
    }
}
