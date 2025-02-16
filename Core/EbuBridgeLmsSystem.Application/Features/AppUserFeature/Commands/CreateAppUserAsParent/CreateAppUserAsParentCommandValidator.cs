using EbuBridgeLmsSystem.Application.Validators.AuthValidators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Features.AppUserFeature.Commands.CreateAppUserAsParent
{
    public class CreateAppUserAsParentCommandValidator : AbstractValidator<CreateAppUserAsParentCommand>

    {
        public CreateAppUserAsParentCommandValidator()
        {
            RuleFor(x => x.RegisterDto)
           .NotNull().WithMessage("RegisterDto is required.")
           .SetValidator(new RegisterValidator());

            RuleFor(x => x.ParentCreateDto)
                .NotNull().WithMessage("ParentCreateDto is required.");
        }
    }
}
