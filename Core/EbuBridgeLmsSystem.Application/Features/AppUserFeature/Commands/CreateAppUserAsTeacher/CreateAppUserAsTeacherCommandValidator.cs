using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EbuBridgeLmsSystem.Application.Validators.AuthValidators;
using FluentValidation;

namespace EbuBridgeLmsSystem.Application.Features.AppUserFeature.Commands.CreateAppUserAsTeacher
{
    public class CreateAppUserAsTeacherCommandValidator:AbstractValidator<CreateAppUserAsTeacherCommand>
    {
        public CreateAppUserAsTeacherCommandValidator()
        {
            RuleFor(x => x.RegisterDto)
                .NotNull().WithMessage("RegisterDto is required.")
                .SetValidator(new RegisterValidator());

            RuleFor(x => x.TeacherCreateDto)
                .NotNull().WithMessage("TeacherCreateDto is required.");
        }
    }
}
