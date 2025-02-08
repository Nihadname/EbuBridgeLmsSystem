using EbuBridgeLmsSystem.Application.Dtos.Auth;
using FluentValidation;

namespace EbuBridgeLmsSystem.Application.Validators.AuthValidators
{
    public class VerifyCodeDtoValidator : AbstractValidator<VerifyCodeDto>
    {
        public VerifyCodeDtoValidator()
        {
            RuleFor(x => x.Code).NotEmpty().MaximumLength(6);
            RuleFor(x => x.Email).NotEmpty();
        }
    }
}
