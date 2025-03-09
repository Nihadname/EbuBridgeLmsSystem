using EbuBridgeLmsSystem.Application.Dtos.Address;
using FluentValidation;

namespace EbuBridgeLmsSystem.Application.Validators.AddressValidators
{
    public class DeleteAddressDtoValidator : AbstractValidator<DeleteAddressDto>
    {
        public DeleteAddressDtoValidator()
        {
            RuleFor(s => s.Id).NotEmpty().NotNull().Must(x => x != Guid.Empty);
        }
    }
}
