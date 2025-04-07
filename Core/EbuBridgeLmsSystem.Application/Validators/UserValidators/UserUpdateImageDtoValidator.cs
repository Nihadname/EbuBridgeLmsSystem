using EbuBridgeLmsSystem.Application.Dtos.Auth;
using FluentValidation;
using System.Linq;

namespace EbuBridgeLmsSystem.Application.Validators.UserValidators
{
    public class UserUpdateImageDtoValidator : AbstractValidator<UserUpdateImageDto>
    {
        public UserUpdateImageDtoValidator()
        {
            RuleFor(s=>s.Image).NotNull().WithMessage("Image file is required");
            RuleFor(s => s).Custom((c, context) =>
            {
                if (c.Image == null) return;
                long maxSizeInBytes = 115 * 1024 * 1024;
                if (c.Image == null || !c.Image.ContentType.Contains("image/"))
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
                if (!allowedContentTypes.Contains(c.Image.ContentType.ToLowerInvariant()))
                {
                    context.AddFailure("Image", "Invalid image format. Allowed formats: JPEG, PNG, GIF, WebP, BMP");
                }
                var extension = Path.GetExtension(c.Image.FileName).ToLowerInvariant();
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp", ".bmp" };
                if (!allowedExtensions.Contains(extension))
                {
                    context.AddFailure("Image", "Invalid file extension. Allowed extensions: .jpg, .jpeg, .png, .gif, .webp, .bmp");
                }
                if (c.Image != null && c.Image.Length > maxSizeInBytes)
                {
                    context.AddFailure("Image", "Data storage exceeds the maximum allowed size of 15 MB");
                }
                if (c.Image.FileName.Length > 100)
                {
                    context.AddFailure("Image", "Filename is too long. Maximum length is 100 characters");
                }
                if (c.Image.FileName.Contains("..") || c.Image.FileName.Contains("/") || c.Image.FileName.Contains("\\"))
                {
                    context.AddFailure("Image", "Filename contains invalid characters");
                }

            });
        }
    }
}
