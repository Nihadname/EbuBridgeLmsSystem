using EbuBridgeLmsSystem.Application.Dtos.Report;
using FluentValidation;

namespace EbuBridgeLmsSystem.Application.Validators.ReportValidators;

public class ReportCreateDtoValidator : AbstractValidator<ReportCreateDto>
{
    public ReportCreateDtoValidator()
    {
        RuleFor(s => s.Description).NotEmpty().MinimumLength(4).MaximumLength(400);
        RuleFor(s => s.ReportOptionId).NotEmpty();
        RuleFor(s => s.ReportedUserId).NotEmpty();
    }
}
