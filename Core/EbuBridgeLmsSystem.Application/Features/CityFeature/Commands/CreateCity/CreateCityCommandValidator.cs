using FluentValidation;

namespace EbuBridgeLmsSystem.Application.Features.CityFeature.Commands.CreateCity
{
    public class CreateCityCommandValidator : AbstractValidator<CreateCityCommand>
    {
        public CreateCityCommandValidator()
        {
            RuleFor(s => s.Name).NotEmpty();
            RuleFor(s=>s.CountryId).NotEmpty().NotNull();
        }
    }
}
