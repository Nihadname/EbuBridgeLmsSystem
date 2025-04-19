using EbuBridgeLmsSystem.Application.Dtos.Course;
using FluentValidation;
using System.Linq;

namespace EbuBridgeLmsSystem.Application.Validators.CourseValidators
{
    public class CourseCreateDtoValidator : AbstractValidator<CourseCreateDto>
    {
        public CourseCreateDtoValidator()
        {


            RuleFor(s => s.Name).MaximumLength(160).MinimumLength(2).NotEmpty();
            RuleFor(s => s.Description).MaximumLength(250).MinimumLength(3).NotEmpty();
            RuleFor(s => s.difficultyLevel).NotNull()
                .IsInEnum().WithMessage("Payment status is invalid.");
            RuleFor(s => s.DurationHours).NotNull();
            RuleFor(s => s.LanguageId).Must(x => x != Guid.Empty).NotEmpty().NotNull();
            RuleFor(s => s.Requirements).NotEmpty();
            RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Salary must be a positive number.").NotEmpty();
         //   RuleFor(x => x.StartDate)
         //      .NotNull()
         //.GreaterThanOrEqualTo(DateTime.UtcNow)
         //.WithMessage("StartDate must be in the future.");

         //   RuleFor(x => x.EndDate)
         //        .NotNull()
         //       .GreaterThanOrEqualTo(DateTime.UtcNow)
         //       .WithMessage("StartDate must be in the future.");
         //   RuleFor(x => x)
         //  .Must(x => x.StartDate <= x.EndDate);
            RuleFor(s => s.formFile).NotNull().WithMessage("Image file is required");
            RuleFor(s => s).Custom((c, context) =>
            {
                if (c.formFile == null) return;
                long maxSizeInBytes = 115 * 1024 * 1024;
                if (c.formFile == null || !c.formFile.ContentType.Contains("image/"))
                {
                    context.AddFailure("Image", "Only image files are accepted");
                }

                var allowedContentTypes = new[] {
                    "image/jpeg",
                    "image/png",
                    "image/gif",
                    "image/webp",
                    "image/bmp"
                };
                if (!allowedContentTypes.Contains(c.formFile.ContentType.ToLowerInvariant()))
                {
                    context.AddFailure("Image", "Invalid image format. Allowed formats: JPEG, PNG, GIF, WebP, BMP");
                }
                var extension = Path.GetExtension(c.formFile.FileName).ToLowerInvariant();
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp", ".bmp" };
                if (!allowedExtensions.Contains(extension))
                {
                    context.AddFailure("Image", "Invalid file extension. Allowed extensions: .jpg, .jpeg, .png, .gif, .webp, .bmp");
                }
                if (c.formFile != null && c.formFile.Length > maxSizeInBytes)
                {
                    context.AddFailure("Image", "Data storage exceeds the maximum allowed size of 15 MB");
                }
                if (c.formFile.FileName.Length > 100)
                {
                    context.AddFailure("Image", "Filename is too long. Maximum length is 100 characters");
                }
                if (c.formFile.FileName.Contains("..") || c.formFile.FileName.Contains("/") || c.formFile.FileName.Contains("\\"))
                {
                    context.AddFailure("Image", "Filename contains invalid characters");
                }

            });
        }

    }
}
