using FluentValidation;

namespace EbuBridgeLmsSystem.Application.Features.ProfileFeature.Commands.UpdateImage
{
    public class UpdateImageCommandValidator : AbstractValidator<UpdateUserImageCommand>
    {
        public UpdateImageCommandValidator()
        {
            RuleFor(s => s.FormFile).NotNull().WithMessage("Image file is required");
            RuleFor(s => s).Custom((c, context) =>
            {
                if (c.FormFile == null) return;
                long maxSizeInBytes = 115 * 1024 * 1024;
                if (c.FormFile == null || !c.FormFile.ContentType.Contains("image/"))
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
                if (!allowedContentTypes.Contains(c.FormFile.ContentType.ToLowerInvariant()))
                {
                    context.AddFailure("Image", "Invalid image format. Allowed formats: JPEG, PNG, GIF, WebP, BMP");
                }
                var extension = Path.GetExtension(c.FormFile.FileName).ToLowerInvariant();
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp", ".bmp" };
                if (!allowedExtensions.Contains(extension))
                {
                    context.AddFailure("Image", "Invalid file extension. Allowed extensions: .jpg, .jpeg, .png, .gif, .webp, .bmp");
                }
                if (c.FormFile != null && c.FormFile.Length > maxSizeInBytes)
                {
                    context.AddFailure("Image", "Data storage exceeds the maximum allowed size of 15 MB");
                }
                if (c.FormFile.FileName.Length > 100)
                {
                    context.AddFailure("Image", "Filename is too long. Maximum length is 100 characters");
                }
                if (c.FormFile.FileName.Contains("..") || c.FormFile.FileName.Contains("/") || c.FormFile.FileName.Contains("\\"))
                {
                    context.AddFailure("Image", "Filename contains invalid characters");
                }

            });
        }
    }
}
