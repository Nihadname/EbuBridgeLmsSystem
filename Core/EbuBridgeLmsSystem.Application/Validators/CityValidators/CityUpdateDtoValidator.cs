using EbuBridgeLmsSystem.Application.Dtos.City;
using FluentValidation;

namespace EbuBridgeLmsSystem.Application.Validators.CityValidators
{
    public class CityUpdateDtoValidator : AbstractValidator<CityUpdateDto>
    {
        public CityUpdateDtoValidator()
        {
            RuleFor(s => s.Id).NotEmpty().NotNull().Must(x => x != Guid.Empty);
        }
    }
}
