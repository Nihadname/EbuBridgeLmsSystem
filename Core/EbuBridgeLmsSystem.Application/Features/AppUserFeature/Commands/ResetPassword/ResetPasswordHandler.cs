using EbuBridgeLmsSystem.Application.Dtos.Auth;
using EbuBridgeLmsSystem.Application.Helpers.Extensions.Auth;
using EbuBridgeLmsSystem.Application.Validators.AuthValidators;
using EbuBridgeLmsSystem.Domain.Entities;
using EbuBridgeLmsSystem.Domain.Entities.Common;
using FluentValidation;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EbuBridgeLmsSystem.Application.Features.AppUserFeature.Commands.ResetPassword
{
    public class ResetPasswordHandler : IRequestHandler<ResetPasswordHandleCommand, Result<Unit>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IValidator<ResetPasswordTokenAndEmailDto> _resetPasswordTokenAndEmailDtoValidator;
        private readonly IValidator<ResetPasswordDto> _resetPasswordDtoValidator;

        public ResetPasswordHandler(UserManager<AppUser> userManager, IValidator<ResetPasswordTokenAndEmailDto> resetPasswordTokenAndEmailDtovalidator, IValidator<ResetPasswordDto> resetPasswordDtoValidator)
        {
            _userManager = userManager;
            _resetPasswordTokenAndEmailDtoValidator = resetPasswordTokenAndEmailDtovalidator;
            _resetPasswordDtoValidator = resetPasswordDtoValidator;
        }

        public async Task<Result<Unit>> Handle(ResetPasswordHandleCommand request, CancellationToken cancellationToken)
        {
            var resetPasswordTokenAndEmailDtoValidator = await _resetPasswordTokenAndEmailDtoValidator.ValidateAsync(request.ResetPasswordTokenAndEmailDto, cancellationToken);
            var resetPasswordDtoValidator = await _resetPasswordDtoValidator.ValidateAsync(request.resetPasswordDto, cancellationToken);
            List<FluentValidation.Results.ValidationResult> validations = new();
            validations.Add(resetPasswordTokenAndEmailDtoValidator);
            validations.Add(resetPasswordDtoValidator);
            if (validations.Any(s => s.IsValid == false))
            {
                var errors = new List<string>();
                foreach (var validationResult in validations)
                {
                    var errorsInValidation = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    foreach (var error in errorsInValidation)
                    {
                        errors.Add(error);
                    }

                }
                return Result<Unit>.Failure(null, errors, ErrorType.ValidationError);
            }
            var isExpiredResult = await CheckExperySutiationOfToken(request.ResetPasswordTokenAndEmailDto);
            if (!isExpiredResult.IsSuccess)
                return Result<Unit>.Failure(isExpiredResult.Error, isExpiredResult.Errors, (ErrorType)isExpiredResult.ErrorType);
            if(!isExpiredResult.Data)
                return Result<Unit>.Failure(Error.Custom("Token", "The token is either invalid or has expired."),null,ErrorType.BusinessLogicError);
            var existedUserResult=await _userManager.GetUserByEmailAsync(request.ResetPasswordTokenAndEmailDto.Email);
            if (!existedUserResult.IsSuccess) return Result<Unit>.Failure(existedUserResult.Error, existedUserResult.Errors, (ErrorType)existedUserResult.ErrorType);
            var isNewOrCurrentPassword = await _userManager.CheckPasswordAsync(existedUserResult.Data, request.resetPasswordDto.Password);
            if (isNewOrCurrentPassword)
            {
                return Result<Unit>.Failure(Error.Custom("Password", "You cannot use your previous password."), null, ErrorType.BusinessLogicError);
            }
            var result = await _userManager.ResetPasswordAsync(existedUserResult.Data, request.ResetPasswordTokenAndEmailDto.Token, request.resetPasswordDto.Password);
            if (!result.Succeeded)
                return Result<Unit>.Failure(Error.SystemError, (List<string>)result.Errors, ErrorType.SystemError);
            await _userManager.UpdateSecurityStampAsync(existedUserResult.Data);
            return Result<Unit>.Success(Unit.Value);
        }
        private async Task<Result<bool>> CheckExperySutiationOfToken(ResetPasswordTokenAndEmailDto resetPasswordTokenAndEmailDto)
        {
            var existUser = await _userManager.FindByEmailAsync(resetPasswordTokenAndEmailDto.Email);
            if (existUser == null)
                return Result<bool>.Failure(Error.NotFound,null, ErrorType.NotFoundError);
            bool result = await _userManager.VerifyUserTokenAsync(
    existUser,
    _userManager.Options.Tokens.PasswordResetTokenProvider,
    "ResetPassword",
    resetPasswordTokenAndEmailDto.Token
);
            return Result<bool>.Success(result);

        }

    }
}
