using EbuBridgeLmsSystem.Application.Dtos.City;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Validators.CityValidators
{
    public class CityDeleteDtoValidator : AbstractValidator<CityDeleteDto>
    {
        public CityDeleteDtoValidator()
        {
            RuleFor(s => s.Id).NotEmpty().NotNull().Must(x => x != Guid.Empty);
        }
    }
}
