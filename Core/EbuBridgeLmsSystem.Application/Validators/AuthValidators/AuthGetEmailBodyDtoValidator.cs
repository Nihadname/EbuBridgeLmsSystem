using EbuBridgeLmsSystem.Application.Dtos.Auth;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Validators.AuthValidators
{
    public class AuthGetEmailBodyDtoValidator : AbstractValidator<AuthGetEmailBodyDto>
    {
        public AuthGetEmailBodyDtoValidator()
        {
            RuleFor(s=>s.Token).NotEmpty();
            RuleFor(s=>s.ApiEndpoint).NotEmpty();
        }
    }
}
