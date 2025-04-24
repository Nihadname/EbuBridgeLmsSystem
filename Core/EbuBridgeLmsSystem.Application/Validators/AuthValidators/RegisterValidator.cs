using EbuBridgeLmsSystem.Application.Dtos.Auth;
using FluentValidation;
using System.Text.RegularExpressions;

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
    .Must(pn => pn != null && Regex.IsMatch(pn.Trim(), @"^\+994(50|51|55|60|70|77|99)\d{7}$"))
    .WithMessage("Invalid Azerbaijani phone number format.");

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
