using EbuBridgeLmsSystem.Application.Dtos.City;
using FluentValidation;

namespace EbuBridgeLmsSystem.Application.Validators.CityValidators
{
    public class CreateCityDtoValidator : AbstractValidator<CreateCityDto>
    {
        public CreateCityDtoValidator()
        {
            RuleFor(s=>s.CountryId).NotEmpty();
            RuleFor(s => s.Name).NotEmpty();
        }
    }
}
