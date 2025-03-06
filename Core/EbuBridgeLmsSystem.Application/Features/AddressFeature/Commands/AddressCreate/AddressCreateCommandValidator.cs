using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Features.AddressFeature.Commands.AddressCreate
{
    public class AddressCreateCommandValidator : AbstractValidator<AddressCreateCommand>
    {
        public AddressCreateCommandValidator()
        {

            RuleFor(s => s.CityId).Must(x => x != Guid.Empty).NotEmpty().NotNull();
            RuleFor(s => s.Region).MaximumLength(250).MinimumLength(1).NotEmpty();
            RuleFor(s => s.Street).MaximumLength(250).MinimumLength(1).NotEmpty();
        }
    }
}
