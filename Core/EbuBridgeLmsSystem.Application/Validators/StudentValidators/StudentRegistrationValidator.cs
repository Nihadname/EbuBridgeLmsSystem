using EbuBridgeLmsSystem.Application.Dtos.Student;
using EbuBridgeLmsSystem.Application.Validators.AuthValidators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Validators.StudentValidators
{
    public class StudentRegistrationValidator : AbstractValidator<StudentRegistrationDto>
    {
        public StudentRegistrationValidator()
        {
            RuleFor(s => s.RegisterDto)
                .NotNull().WithMessage("RegisterDto cannot be null")
                .SetValidator(new RegisterValidator());
        }
    }
}
