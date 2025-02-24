using EbuBridgeLmsSystem.Application.Dtos.Auth;
using EbuBridgeLmsSystem.Application.Exceptions;
using EbuBridgeLmsSystem.Application.Helpers.Extensions.Auth;
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
            var existedUserResult=await _userManager.GetUserByEmailAsync(request.ResetPasswordTokenAndEmailDto.Email);
            if (!existedUserResult.IsSuccess) return Result<Unit>.Failure(existedUserResult.ErrorKey, existedUserResult.Message, existedUserResult.Errors, (ErrorType)existedUserResult.ErrorType);
            var isNewOrCurrentPassword = await _userManager.CheckPasswordAsync(existedUserResult.Data, request.resetPasswordDto.Password);
            if (isNewOrCurrentPassword)
            {
                return Result<Unit>.Failure("Password", "You cannot use your previous password.", null, ErrorType.BusinessLogicError);
            }
            var result = await _userManager.ResetPasswordAsync(existedUserResult.Data, request.ResetPasswordTokenAndEmailDto.Token, request.resetPasswordDto.Password);
            if (!result.Succeeded)
                return Result<Unit>.Failure("ResetPassword", null, (List<string>)result.Errors, ErrorType.SystemError);
            await _userManager.UpdateSecurityStampAsync(existedUserResult.Data);
            return Result<Unit>.Success(Unit.Value);
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
