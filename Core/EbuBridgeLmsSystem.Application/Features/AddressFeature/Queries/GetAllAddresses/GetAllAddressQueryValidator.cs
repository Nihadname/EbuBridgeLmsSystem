using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Features.AddressFeature.Queries.GetAllAddresses
{
    public class GetAllAddressQueryValidator : AbstractValidator<GetAllAddressQuery>
    {
        public GetAllAddressQueryValidator()
        {
            RuleFor(s => s.Limit).NotNull();
        }
    }
}
