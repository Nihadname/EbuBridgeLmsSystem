using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Features.AppUserFeature.Commands.UserSoftDelete
{
    public class UserSoftDeleteCommandValidator : AbstractValidator<UserSoftDeleteCommand>
    {
        public UserSoftDeleteCommandValidator()
        {
            RuleFor(s => s.Id).NotEmpty().NotNull();
        }
    }
}
