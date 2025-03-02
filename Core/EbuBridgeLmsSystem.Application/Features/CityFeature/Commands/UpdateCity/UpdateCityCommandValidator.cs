using FluentValidation;

namespace EbuBridgeLmsSystem.Application.Features.CityFeature.Commands.UpdateCity
{
    public class UpdateCityCommandValidator : AbstractValidator<UpdateCityCommand>
    {
        public UpdateCityCommandValidator()
        {
            RuleFor(s=>s.Id).NotEmpty().NotNull().Must(x => x != Guid.Empty);
        }
    }
}
