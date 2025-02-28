using EbuBridgeLmsSystem.Application.Dtos.Country;
using FluentValidation;

namespace EbuBridgeLmsSystem.Application.Validators.CountryValidators
{
    public class CountryDeleteDtoValidator : AbstractValidator<CountryDeleteDto>
    {
        public CountryDeleteDtoValidator()
        {
            RuleFor(s=>s.Id).Must(x => x != Guid.Empty).NotNull()
        .NotEmpty();
        }
        
    }
}
