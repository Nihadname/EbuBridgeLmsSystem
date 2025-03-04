using EbuBridgeLmsSystem.Application.Dtos.Auth;
using FluentValidation;

namespace EbuBridgeLmsSystem.Application.Validators.AuthValidators
{
    public class ResetPasswordTokenAndEmailDtoValidator : AbstractValidator<ResetPasswordTokenAndEmailDto>
    {
        public ResetPasswordTokenAndEmailDtoValidator()
        {

            RuleFor(s => s.Email).NotEmpty().EmailAddress();
            RuleFor(s => s.Token).NotEmpty();
        }
    }
}
