using FluentValidation;

namespace EbuBridgeLmsSystem.Application.Features.CourseFeature.Commands.CourseCreate
{
    public class CourseCreateCommandValidator : AbstractValidator<CourseCreateCommand>
    {
        public CourseCreateCommandValidator()
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
            RuleFor(x => x.formFile).NotNull();   
            RuleFor(s=>s.MaxAmountOfPeople).NotNull();
            RuleFor(s => s).Custom((c, context) =>
            {
                long maxSizeInBytes = 15 * 1024 * 1024;
                if (c.formFile == null || !c.formFile.ContentType.Contains("image/"))
                {
                    context.AddFailure("Image", "Only image files are accepted");
                    return;
                }

                if (c.formFile != null && c.formFile.Length > maxSizeInBytes)
                {
                    context.AddFailure("Image", "Data storage exceeds the maximum allowed size of 15 MB");
                }

            });
        }
    }
}
