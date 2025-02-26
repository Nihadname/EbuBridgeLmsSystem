using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Features.CountryFeature.Commands.CreateCountry
{
    public class CreateCountryHandler : IRequestHandler<CreateCountryCommand, Result<Unit>>
    {
        public Task<Result<Unit>> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
