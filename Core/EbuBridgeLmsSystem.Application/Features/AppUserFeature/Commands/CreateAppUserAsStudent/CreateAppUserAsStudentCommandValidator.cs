using EbuBridgeLmsSystem.Application.Dtos.Auth;
using EbuBridgeLmsSystem.Application.Dtos.Student;
using EbuBridgeLmsSystem.Application.Validators.AuthValidators;
using FluentValidation;

namespace EbuBridgeLmsSystem.Application.Features.AppUserFeature.Commands.CreateAppUserAsStudent
{
    public class CreateAppUserAsStudentCommandValidator : AbstractValidator<CreateAppUserAsStudentCommand>
    {
        public CreateAppUserAsStudentCommandValidator()
        {
            RuleFor(x => x.RegisterDto)
            .NotNull().WithMessage("RegisterDto is required.")
            .Must((command, dto) => RenewRegisterDto(command)).WithMessage("RegisterDto was renewed.")
            .SetValidator(new RegisterValidator());

            RuleFor(x => x.StudentCreateDto)
             .Must((command, dto) => RenewStudentDto(command)).WithMessage("RegisterDto was renewed.")
                .NotNull().WithMessage("StudentCreateDto is required.");


        }
        private bool RenewRegisterDto(CreateAppUserAsStudentCommand command)
        {
            if (command.RegisterDto == null)
            {
                command = command with { RegisterDto = new RegisterDto() };
                return true;
            }
            return false;
        }

        private bool RenewStudentDto(CreateAppUserAsStudentCommand command)
        {
            if (command.StudentCreateDto == null)
            {
                command = command with { StudentCreateDto = new StudentCreateDto() };
                return true;
            }
            return false;
        }
    }
}
