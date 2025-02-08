using EbuBridgeLmsSystem.Application.Dtos.Fee;
using FluentValidation;

namespace EbuBridgeLmsSystem.Application.Validators.FeeValidators
{
    public class FeeHandleDtoValidator : AbstractValidator<FeeHandleDto>
    {
        public FeeHandleDtoValidator()
        {
            RuleFor(x => x.Description)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(160)
            .When(x => x.Description != null);
            RuleFor(s => s.PaymentMethodId).NotEmpty();
        }
    }
}
