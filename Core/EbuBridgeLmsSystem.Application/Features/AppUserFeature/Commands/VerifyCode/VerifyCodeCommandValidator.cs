using FluentValidation;

namespace EbuBridgeLmsSystem.Application.Features.AppUserFeature.Commands.VerifyCode
{
    public class VerifyCodeCommandValidator : AbstractValidator<VerifyCodeCommand>
    {
        public VerifyCodeCommandValidator()
        {
            RuleFor(s => s.Email).NotEmpty().EmailAddress().MaximumLength(200).WithMessage("max is 200");
            RuleFor(s => s.Code).NotEmpty().MaximumLength(6);
        }
    }
}
