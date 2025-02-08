using EbuBridgeLmsSystem.Application.Dtos.Lesson;
using FluentValidation;

namespace EbuBridgeLmsSystem.Application.Validators.LessonValidators
{
    public class LessonCreateDtoValidator : AbstractValidator<LessonCreateDto>
    {
        public LessonCreateDtoValidator()
        {
        }
    }
}
