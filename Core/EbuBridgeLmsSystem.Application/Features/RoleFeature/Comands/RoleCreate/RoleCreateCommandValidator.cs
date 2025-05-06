using FluentValidation;

namespace EbuBridgeLmsSystem.Application.Features.RoleFeature.Comands.RoleCreate
{
    public sealed class RoleCreateCommandValidator : AbstractValidator<RoleCreateCommand>
    {
        public RoleCreateCommandValidator()
        {
            RuleFor(s=>s.Name).NotEmpty().MaximumLength(100);
        }
    }
}
