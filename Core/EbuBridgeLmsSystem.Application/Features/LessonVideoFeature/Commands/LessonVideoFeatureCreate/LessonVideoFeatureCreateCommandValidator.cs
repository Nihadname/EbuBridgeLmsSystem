using FluentValidation;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace EbuBridgeLmsSystem.Application.Features.LessonVideoFeature.Commands.LessonVideoFeatureCreate
{
    public sealed class LessonVideoFeatureCreateCommandValidator : AbstractValidator<LessonVideoFeatureCreateCommand>
    {
        public LessonVideoFeatureCreateCommandValidator()
        {
            RuleFor(s => s.Title).NotEmpty().WithMessage("Title is required")
                .MinimumLength(2).WithMessage("Title must be at least 2 characters")
                .MaximumLength(100).WithMessage("Title cannot exceed 100 characters");
            RuleFor(s => s).Custom((c, context) =>
            {
                if(c.File == null)
                {
                    return;
                }
                    long maxSizeInBytes = 15 * 1024 * 1024;
                
                var allowedVideoTypes = new List<string>
                {
                    "video/mp4",
                    "video/mpeg",
                    "video/webm",
                    "video/ogg"
                };
                if (!allowedVideoTypes.Contains(c.File.ContentType.ToLower()))
                {
                    context.AddFailure("File", $"Only the following video formats are supported: {string.Join(", ", allowedVideoTypes.Select(t => t.Replace("video/", "")))}");
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
            RuleFor(s => s.File).NotNull().WithMessage("Video file is required");
            RuleFor(s => s.LessonId).NotEmpty().WithMessage("Lesson ID is required")
                .Must(x => x != Guid.Empty).WithMessage("Lesson ID cannot be empty");
        }
    }
}
