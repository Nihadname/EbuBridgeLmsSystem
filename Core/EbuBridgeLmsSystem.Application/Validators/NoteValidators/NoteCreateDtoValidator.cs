using EbuBridgeLmsSystem.Application.Dtos.Note;
using FluentValidation;

namespace EbuBridgeLmsSystem.Application.Validators.NoteValidators
{
    public class NoteCreateDtoValidator : AbstractValidator<NoteCreateDto>
    {
        public NoteCreateDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MinimumLength(2).MaximumLength(50).WithMessage("Title must be at least 10 characters long.");
            RuleFor(x => x.Description).NotEmpty().MinimumLength(3).MaximumLength(250).WithMessage("Description must be at least 3 characters long.");
            RuleFor(x => x.CategoryName)
              .NotEmpty().WithMessage("CategoryName is required.");
        }
    }
}
