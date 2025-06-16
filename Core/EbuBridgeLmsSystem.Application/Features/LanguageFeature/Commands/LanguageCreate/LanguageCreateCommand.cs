using FluentValidation;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;

namespace EbuBridgeLmsSystem.Application.Features.LanguageFeature.Commands.LanguageCreate;

public sealed record LanguageCreateCommand: IRequest<Result<Unit>>
{
    public string Name { get; init; }
}

public class LanguageCreateCommandValidator : AbstractValidator<LanguageCreateCommand>
{
    public LanguageCreateCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
    }
}