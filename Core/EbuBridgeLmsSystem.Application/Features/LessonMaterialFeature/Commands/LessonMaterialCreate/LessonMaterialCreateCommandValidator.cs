using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Features.LessonMaterialFeature.Commands.LessonMaterialCreate
{
    public sealed class LessonMaterialCreateCommandValidator : AbstractValidator<LessonMaterialCreateCommand>
    {
        public LessonMaterialCreateCommandValidator()
        {
            RuleFor(s => s.Title).NotEmpty().MinimumLength(2).MaximumLength(100);
            RuleFor(s => s.File).NotNull().WithMessage("Image file is required");
            RuleFor(s => s).Custom((c, context) =>
            {
                if (c.File == null) return;
                long maxSizeInBytes = 115 * 1024 * 1024;
                if (c.File == null || !c.File.ContentType.Contains("image/"))
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
                if (!allowedContentTypes.Contains(c.File.ContentType.ToLowerInvariant()))
                {
                    context.AddFailure("Image", "Invalid image format. Allowed formats: JPEG, PNG, GIF, WebP, BMP");
                }
                var extension = Path.GetExtension(c.File.FileName).ToLowerInvariant();
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp", ".bmp" };
                if (!allowedExtensions.Contains(extension))
                {
                    context.AddFailure("Image", "Invalid file extension. Allowed extensions: .jpg, .jpeg, .png, .gif, .webp, .bmp");
                }
                if (c.File != null && c.File.Length > maxSizeInBytes)
                {
                    context.AddFailure("Image", "Data storage exceeds the maximum allowed size of 15 MB");
                }
                if (c.File.FileName.Length > 100)
                {
                    context.AddFailure("Image", "Filename is too long. Maximum length is 100 characters");
                }
                if (c.File.FileName.Contains("..") || c.File.FileName.Contains("/") || c.File.FileName.Contains("\\"))
                {
                    context.AddFailure("Image", "Filename contains invalid characters");
                }

            });
            RuleFor(s => s.LessonId).Must(x => x != Guid.Empty).NotNull()
        .NotEmpty();
        }
    }
}
