using EbuBridgeLmsSystem.Application.Dtos.Auth;
using EbuBridgeLmsSystem.Application.Features.AppUser.Commands.CreateAppUserAsStudent;
using FluentValidation;

namespace EbuBridgeLmsSystem.Application.Validators.AuthValidators
{
    public class RegisterValidator : AbstractValidator<RegisterDto>
    {
        public RegisterValidator()
        {
            RuleFor(s => s.UserName).NotEmpty().WithMessage("not empty")
                .MaximumLength(100).WithMessage("max is 100");

            RuleFor(s => s.FullName).NotEmpty().WithMessage("not empty")
                .MaximumLength(150).WithMessage("max is 150");

            RuleFor(s => s.Email).NotEmpty().WithMessage("not empty")
                .MaximumLength(200).WithMessage("max is 200")
                .EmailAddress().WithMessage("should be in email format");
            RuleFor(s => s.Password).NotEmpty().WithMessage("not empty")
                .MinimumLength(8)
                .MaximumLength(100).WithMessage("max is 100");
            RuleFor(s => s.RepeatPassword).NotEmpty().WithMessage("not empty")
                                .MinimumLength(8);
            RuleFor(x => x.PhoneNumber)
              .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^(\+?\d{1,4}?)[\s.-]?\(?\d{1,4}?\)?[\s.-]?\d{1,4}[\s.-]?\d{1,9}$")
            .WithMessage("Invalid phone number format.");
            RuleFor(s => s).Custom((s, context) =>
            {
                if (s.Password != s.RepeatPassword)
                {
                    context.AddFailure("Password", "paswords dont match in this part");
                }

            });
            RuleFor(x => x.BirthDate)
            .Must(date => DateTime.UtcNow.Year - date.Year >= 15);

        }
    }
}
