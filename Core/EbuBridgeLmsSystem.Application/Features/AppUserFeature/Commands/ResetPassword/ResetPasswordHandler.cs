using EbuBridgeLmsSystem.Application.Dtos.Auth;
using EbuBridgeLmsSystem.Application.Exceptions;
using EbuBridgeLmsSystem.Domain.Entities;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Features.AppUserFeature.Commands.ResetPassword
{
    public class ResetPasswordHandler : IRequestHandler<ResetPasswordHandleCommand, Result<Unit>>
    {
        private readonly UserManager<AppUser> _userManager;
        public async Task<Result<Unit>> Handle(ResetPasswordHandleCommand request, CancellationToken cancellationToken)
        {
            var isExpired = await CheckExperySutiationOfToken(request.ResetPasswordTokenAndEmailDto);
            if(!isExpired)
                return Result<Unit>.Failure("Token","The token is either invalid or has expired.",null,ErrorType.SystemError);


        }
        private async Task<bool> CheckExperySutiationOfToken(ResetPasswordTokenAndEmailDto resetPasswordTokenAndEmailDto)
        {
            var existUser = await _userManager.FindByEmailAsync(resetPasswordTokenAndEmailDto.Email);
            if (existUser == null) throw new CustomException(404, "User is null or empty");
            bool result = await _userManager.VerifyUserTokenAsync(
    existUser,
    _userManager.Options.Tokens.PasswordResetTokenProvider,
    "ResetPassword",
    resetPasswordTokenAndEmailDto.Token
);
            return result;

        }

    }
}
