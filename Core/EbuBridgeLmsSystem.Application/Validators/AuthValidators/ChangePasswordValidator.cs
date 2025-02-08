using EbuBridgeLmsSystem.Application.Dtos.Auth;
using FluentValidation;

namespace EbuBridgeLmsSystem.Application.Validators.AuthValidators
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordDto>
    {
        public ChangePasswordValidator()
        {
            RuleFor(s => s.CurrentPassword).NotEmpty().WithMessage("not empty")
                .MinimumLength(8);
            RuleFor(s => s.NewPassword).NotEmpty().WithMessage("not empty")
              .MinimumLength(8);
            RuleFor(s => s.ConfirmPassword).NotEmpty().WithMessage("not empty")
         .MinimumLength(8);
            RuleFor(s => s.NewPassword).Equal(s => s.ConfirmPassword);
        }
    }
}
