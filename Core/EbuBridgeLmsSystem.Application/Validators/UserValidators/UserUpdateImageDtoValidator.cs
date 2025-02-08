using EbuBridgeLmsSystem.Application.Dtos.Auth;
using FluentValidation;

namespace EbuBridgeLmsSystem.Application.Validators.UserValidators
{
    public class UserUpdateImageDtoValidator : AbstractValidator<UserUpdateImageDto>
    {
        public UserUpdateImageDtoValidator()
        {
            RuleFor(s => s).Custom((c, context) =>
            {
                long maxSizeInBytes = 115 * 1024 * 1024;
                if (c.Image == null || !c.Image.ContentType.Contains("image/"))
                {
                    context.AddFailure("Image", "Only image files are accepted");
                }

                if (c.Image != null && c.Image.Length > maxSizeInBytes)
                {
                    context.AddFailure("Image", "Data storage exceeds the maximum allowed size of 15 MB");
                }

            });
        }
    }
}
