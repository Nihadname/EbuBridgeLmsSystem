using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Features.CityFeature.Queries.GetByIdCity
{
    public class GetByIdCityQueryValidator : AbstractValidator<GetByIdCityQuery>
    {
        public GetByIdCityQueryValidator()
        {
            RuleFor(s => s.Id).Must(x => x != Guid.Empty).NotEmpty().NotNull();
        }
    }
}
