using EbuBridgeLmsSystem.Application.Helpers.Extensions;
using EbuBridgeLmsSystem.Domain.Entities;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EbuBridgeLmsSystem.Application.Features.AppUserFeature.Commands.VerifyCode
{
    public class VerifyCodeHandler : IRequestHandler<VerifyCodeCommand, Result<string>>
    {
        private readonly UserManager<AppUser> _userManager;

        public VerifyCodeHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result<string>> Handle(VerifyCodeCommand request, CancellationToken cancellationToken)
        {
            var existedUser = await _userManager.FindByEmailAsync(request.Email);
            if (existedUser is null) return Result<string>.Failure("User", "User is null",null, ErrorType.NotFoundError);
            if (!IsVerificationCodeValid(request.Code, existedUser))
                return Result<string>.Failure("Code", "Invalid or expired verification code.", null, ErrorType.BusinessLogicError);
           await UpdateUserVerificationStatusAsync(existedUser);
            return Result<string>.Success("Code verified successfully. You can now log in.");
        }
        private bool IsVerificationCodeValid(string code, AppUser existedUser)
        {
            return HashExtension.VerifyHash(code, existedUser.Salt, existedUser.VerificationCode)
                   && existedUser.ExpiredDate >= DateTime.UtcNow;
        }
        private async Task UpdateUserVerificationStatusAsync(AppUser existedUser)
        {
            existedUser.IsEmailVerificationCodeValid = true;
            existedUser.VerificationCode = null;
            existedUser.ExpiredDate = null;
            await _userManager.UpdateAsync(existedUser);
        }

    }
}
