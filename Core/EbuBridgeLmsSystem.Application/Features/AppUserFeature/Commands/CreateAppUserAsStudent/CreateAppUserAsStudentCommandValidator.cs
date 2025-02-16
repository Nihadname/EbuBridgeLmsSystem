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
            .SetValidator(new RegisterValidator());

            RuleFor(x => x.StudentCreateDto)
                .NotNull().WithMessage("StudentCreateDto is required.");


        }
        // private bool RenewRegisterDto(CreateAppUserAsStudentCommand command)
        // {
        //     if (command.RegisterDto == null)
        //     {
        //         command = command with { RegisterDto = new RegisterDto() };
        //         return true;
        //     }
        //     return false;
        // }
        //
        // private bool RenewStudentDto(CreateAppUserAsStudentCommand command)
        // {
        //     if (command.StudentCreateDto == null)
        //     {
        //         command = command with { StudentCreateDto = new StudentCreateDto() };
        //         return true;
        //     }
        //     return false;
        // }
    }
}
