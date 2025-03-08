using EbuBridgeLmsSystem.Application.Features.AddressFeature.Commands.UpdateAdress;
using FluentValidation;

namespace EbuBridgeLmsSystem.Application.Features.AddressFeature.Commands.UpdateAddress
{
    public class UpdateAddressCommandValidator : AbstractValidator<UpdateAddressCommand>
    {
        public UpdateAddressCommandValidator()
        {
            RuleFor(s => s.Id).NotEmpty().NotNull().Must(x => x != Guid.Empty);
        }
    }
}
