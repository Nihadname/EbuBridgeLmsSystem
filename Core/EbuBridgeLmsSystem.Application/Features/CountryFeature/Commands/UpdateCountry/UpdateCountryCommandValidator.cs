using FluentValidation;

namespace EbuBridgeLmsSystem.Application.Features.CountryFeature.Commands.UpdateCountry
{
    public class UpdateCountryCommandValidator : AbstractValidator<UpdateCountryCommand>
    {
        public UpdateCountryCommandValidator()
        {
            RuleFor(s => s.Name).NotEmpty();
            RuleFor(s=>s.Id).NotEmpty();
        }
    }
}
