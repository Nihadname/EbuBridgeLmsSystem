using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Features.LessonStudentFeature.Commands.LessonStudentCreate
{
    public sealed class LessonStudentCreateCommandValidator : AbstractValidator<LessonStudentCreateCommand>
    {
        public LessonStudentCreateCommandValidator()
        {
            RuleFor(x => x.LessonId).NotEmpty().Must(x => x != Guid.Empty).NotNull();
            RuleFor(x => x.StudentId).NotEmpty().Must(x => x != Guid.Empty).NotNull();
        }
    }
}
