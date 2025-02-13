using EbuBridgeLmsSystem.Application.Dtos.Auth;
using FluentValidation;

namespace EbuBridgeLmsSystem.Application.Validators.AuthValidators
{
    public class SendVerificationCodeDtoValidator : AbstractValidator<SendVerificationCodeDto>
    {
        public SendVerificationCodeDtoValidator()
        {
            RuleFor(s => s.Email).NotEmpty().EmailAddress();
        }
    }
}
