using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Features.AppUserFeature.Commands.ForgotPassword
{
    public class ForgotPasswordCommandValidator : AbstractValidator<ForgetPasswordCommand>
    {
        public ForgotPasswordCommandValidator()
        {
            RuleFor(s => s.Email).NotEmpty().EmailAddress().MaximumLength(250);
           

        }
    }
}
