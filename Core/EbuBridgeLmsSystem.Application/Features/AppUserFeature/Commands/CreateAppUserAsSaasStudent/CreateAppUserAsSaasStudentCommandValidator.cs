using EbuBridgeLmsSystem.Application.Validators.AuthValidators;
using FluentValidation;

namespace EbuBridgeLmsSystem.Application.Features.AppUserFeature.Commands.CreateAppUserAsSaasStudent
{
    public sealed class CreateAppUserAsSaasStudentCommandValidator : AbstractValidator<CreateAppUserAsSaasStudentCommand>
    {
        public CreateAppUserAsSaasStudentCommandValidator()
        {
            RuleFor(x => x.RegisterDto)
          .NotNull().WithMessage("RegisterDto is required.")
          .SetValidator(new RegisterValidator());

        }
    }
}
