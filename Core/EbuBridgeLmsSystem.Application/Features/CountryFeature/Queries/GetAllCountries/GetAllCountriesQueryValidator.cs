using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Features.CountryFeature.Queries.GetAllCountries
{
    public class GetAllCountriesQueryValidator : AbstractValidator<GetAllCountriesQuery>
    {
        public GetAllCountriesQueryValidator()
        {
            RuleFor(s => s.Limit).NotNull();
        }
    }
}
