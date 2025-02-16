using EbuBridgeLmsSystem.Application.Dtos.Teacher;
using FluentValidation;

namespace EbuBridgeLmsSystem.Application.Validators.TeacherValidators
{
    public class TeacherCreateDtoValidator : AbstractValidator<TeacherCreateDto>
    {
        public TeacherCreateDtoValidator()
        {
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MinimumLength(10).WithMessage("Description must be at least 10 characters long.");

           

            RuleFor(x => x.experience)
                .GreaterThanOrEqualTo(0).WithMessage("Experience must be a non-negative number.");

            

            RuleFor(x => x.Position)
                .NotEmpty().WithMessage("Position is required.");

            RuleFor(x => x.Salary)
                .GreaterThan(0).WithMessage("Salary must be a positive number.");

            RuleFor(x => x.FaceBookUrl)
                .Must(BeAValidUrl).WithMessage("Facebook URL must be valid.")
                .When(x => !string.IsNullOrWhiteSpace(x.FaceBookUrl));

            RuleFor(x => x.pinterestUrl)
                .Must(BeAValidUrl).WithMessage("Pinterest URL must be valid.")
                .When(x => !string.IsNullOrWhiteSpace(x.pinterestUrl));

            RuleFor(x => x.SkypeUrl)
                .Must(BeAValidUrl).WithMessage("Skype URL must be valid.")
                .When(x => !string.IsNullOrWhiteSpace(x.SkypeUrl));

            RuleFor(x => x.IntaUrl)
                .Must(BeAValidUrl).WithMessage("Instagram URL must be valid.")
                .When(x => !string.IsNullOrWhiteSpace(x.IntaUrl));
        }

        private bool BeAValidUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out _) &&
                   (url.StartsWith("http://") || url.StartsWith("https://"));
        }
    }
}
