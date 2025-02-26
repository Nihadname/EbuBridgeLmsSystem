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
            RuleFor(s => s.Country).MaximumLength(160).MinimumLength(1).NotEmpty();
            RuleFor(s => s.City).MaximumLength(250).MinimumLength(1).NotEmpty();
            RuleFor(s => s.Region).MaximumLength(250).MinimumLength(1).NotEmpty();
            RuleFor(s => s.Street).MaximumLength(250).MinimumLength(1).NotEmpty();
        }
    }
}
