using EbuBridgeLmsSystem.Application.Dtos.Auth;
using FluentValidation;

namespace EbuBridgeLmsSystem.Application.Validators.AuthValidators
{
    public class AuthGetEmailDtoValidator : AbstractValidator<AuthGetEmailDto>
    {
        public AuthGetEmailDtoValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
          
        }
    }
}
