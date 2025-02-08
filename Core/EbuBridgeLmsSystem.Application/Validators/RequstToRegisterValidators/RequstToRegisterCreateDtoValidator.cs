using EbuBridgeLmsSystem.Application.Dtos.RequstToRegister;
using FluentValidation;

namespace EbuBridgeLmsSystem.Application.Validators.RequstToRegisterValidators
{
    public class RequstToRegisterCreateDtoValidator : AbstractValidator<RequstToRegisterCreateDto>
    {
        public RequstToRegisterCreateDtoValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("FullName is required.")
                .MinimumLength(10).MaximumLength(90).WithMessage("FullName must be at least 10 characters long.");
            RuleFor(x => x.Age).InclusiveBetween(15, 65).NotEmpty();
            RuleFor(x => x.PhoneNumber)
              .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^(\+?\d{1,4}?)[\s.-]?\(?\d{1,4}?\)?[\s.-]?\d{1,4}[\s.-]?\d{1,9}$")
            .WithMessage("Invalid phone number format.");
            RuleFor(s => s.Email).NotEmpty().WithMessage("not empty")
                .MaximumLength(300).WithMessage("max is 300").EmailAddress();
            RuleFor(s => s.ChoosenCourse)
                .NotEmpty().WithMessage("ChoosenCourse cannot be empty.");
            RuleFor(s => s.ChildAge).InclusiveBetween(15, 18);
            RuleFor(s => s.ChildName).MaximumLength(100).WithMessage("max is 100").When(s => !string.IsNullOrWhiteSpace(s.ChildName) || s.ChildName != null);
        }
    }
}