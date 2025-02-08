using EbuBridgeLmsSystem.Application.Dtos.Auth;
using FluentValidation;

namespace EbuBridgeLmsSystem.Application.Validators.AuthValidators
{
    public class ResetPasswordDtoValidator : AbstractValidator<ResetPasswordDto>
    {
        public ResetPasswordDtoValidator()
        {
            RuleFor(s => s.Password).NotEmpty().WithMessage("not empty")
                .MinimumLength(8)
                .MaximumLength(100).WithMessage("max is 100");
            RuleFor(s => s.RePassword).NotEmpty().WithMessage("not empty")
                                .MinimumLength(8)
           .MaximumLength(100).WithMessage("max is 100");
            RuleFor(s => s.Password).Equal(s => s.RePassword);
        }
    }
}
