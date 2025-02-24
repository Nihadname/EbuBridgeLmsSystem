using FluentValidation;

namespace EbuBridgeLmsSystem.Application.Features.AppUserFeature.Commands.ChangePassword
{
    public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordCommandValidator()
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
