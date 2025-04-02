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
            RuleFor(s => s).Custom((c, context) =>
            {
                long maxSizeInBytes = 15 * 1024 * 1024;


                if (c.File != null && c.File.Length > maxSizeInBytes)
                {
                    context.AddFailure("Image", "Data storage exceeds the maximum allowed size of 15 MB");
                }
            });
            RuleFor(s => s.File).NotNull();
            RuleFor(s => s.LessonId).Must(x => x != Guid.Empty).NotNull()
        .NotEmpty();
        }
    }
}
