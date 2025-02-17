using FluentValidation;

namespace EbuBridgeLmsSystem.Application.Features.AppUserFeature.Commands.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(s => s.UserNameOrGmail).NotEmpty().WithMessage("not empty")
               .MaximumLength(100).WithMessage("max is 200");
            RuleFor(s => s.Password).NotEmpty().WithMessage("not empty")
                .MinimumLength(8)
                .MaximumLength(100).WithMessage("max is 100");
        }
    }
}
