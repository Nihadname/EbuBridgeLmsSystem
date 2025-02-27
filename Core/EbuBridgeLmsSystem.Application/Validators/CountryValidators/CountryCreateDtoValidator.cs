using EbuBridgeLmsSystem.Application.Dtos.Country;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Validators.CountryValidators
{
    public class CountryCreateDtoValidator : AbstractValidator<CountryCreateDto>
    {
        public CountryCreateDtoValidator()
        {
            RuleFor(s=>s.Name).NotEmpty();
        }
    }
}
