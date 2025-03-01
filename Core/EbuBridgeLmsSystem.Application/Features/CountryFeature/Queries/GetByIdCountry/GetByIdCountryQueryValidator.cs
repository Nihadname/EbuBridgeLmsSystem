using FluentValidation;

namespace EbuBridgeLmsSystem.Application.Features.CountryFeature.Queries.GetByIdCountry
{
    public class GetByIdCountryQueryValidator : AbstractValidator<GetByIdCountryQuery>
    {
        public GetByIdCountryQueryValidator()
        {
            RuleFor(s=>s.Id).Must(x => x != Guid.Empty).NotEmpty().NotNull();
        }
    }
}
