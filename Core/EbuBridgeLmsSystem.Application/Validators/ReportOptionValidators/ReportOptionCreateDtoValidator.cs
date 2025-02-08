using EbuBridgeLmsSystem.Application.Dtos.ReportOption;
using FluentValidation;

namespace EbuBridgeLmsSystem.Application.Validators.ReportOptionValidators
{
    public class ReportOptionCreateDtoValidator : AbstractValidator<ReportOptionCreateDto>
    {
        public ReportOptionCreateDtoValidator()
        {
            RuleFor(s => s.Name).NotEmpty().MaximumLength(300);
        }
    }
}
