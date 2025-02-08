using EbuBridgeLmsSystem.Application.Dtos.Auth;
using FluentValidation;

namespace EbuBridgeLmsSystem.Application.Validators.AuthValidators
{
    public class ResetPasswordEmailDtoValidator : AbstractValidator<ResetPasswordEmailDto>
    {
        public ResetPasswordEmailDtoValidator()
        {
            RuleFor(s => s.Email).NotEmpty().WithMessage("not empty")
             .MinimumLength(8);
        }
    }
}
