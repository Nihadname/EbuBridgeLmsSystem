using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Features.AppUserFeature.Commands.RevokeRefreshToken
{
    public class RevokeRefreshTokenCommand:IRequest<Result<Unit>>
    {
    }
}
