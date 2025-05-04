using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Features.LessonStudentFeature.Commands.LessonStudentCreate
{
    public sealed class LessonStudentTeacherCreateCommandValidator : AbstractValidator<LessonStudentTeacherCreateCommand>
    {
        public LessonStudentTeacherCreateCommandValidator()
        {
            RuleFor(x => x.LessonId).NotEmpty().Must(x => x != Guid.Empty).NotNull();
            RuleFor(x => x.StudentId).NotEmpty().Must(x => x != Guid.Empty).NotNull();
        }
    }
}
