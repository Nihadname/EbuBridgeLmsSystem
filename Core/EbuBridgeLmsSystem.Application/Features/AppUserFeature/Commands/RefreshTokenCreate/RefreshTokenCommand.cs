using EbuBridgeLmsSystem.Application.Dtos.Auth;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Features.AppUserFeature.Commands.RefreshTokenCreate
{
    public record RefreshTokenCommand:IRequest<Result<AuthRefreshTokenResponseDto>>
    {
    }
}
