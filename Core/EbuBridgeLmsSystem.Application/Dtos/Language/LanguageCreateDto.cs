using FluentValidation;

namespace EbuBridgeLmsSystem.Application.Dtos.Language;

public sealed record LanguageCreateDto
{
    public string Name { get; init; }
    
}

public sealed class LanguageCreateDtoValidator : AbstractValidator<LanguageCreateDto>
{
    public LanguageCreateDtoValidator()
    {
        RuleFor(x => x.Name).NotNull().NotEmpty();
    }
}