using EbuBridgeLmsSystem.Application.Dtos.Fee;
using FluentValidation;

namespace EbuBridgeLmsSystem.Application.Validators.FeeValidators
{
    public class FeeCreateDtoValidator : AbstractValidator<FeeCreateDto>
    {
        public FeeCreateDtoValidator()
        {
            RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Salary must be a positive number.").NotEmpty();
            RuleFor(s => s.DueDate).NotNull().GreaterThanOrEqualTo(DateTime.Today);
            RuleFor(s => s.PaymentStatus).NotNull()
                .IsInEnum().WithMessage("Payment status is invalid.");
            RuleFor(s => s.StudentId).NotNull().NotEmpty();
        }
    }
}
