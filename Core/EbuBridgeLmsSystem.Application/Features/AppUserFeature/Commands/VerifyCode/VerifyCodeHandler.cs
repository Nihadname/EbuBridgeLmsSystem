using EbuBridgeLmsSystem.Application.Helpers.Extensions;
using EbuBridgeLmsSystem.Domain.Entities;
using EbuBridgeLmsSystem.Domain.Entities.Common;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace EbuBridgeLmsSystem.Application.Features.AppUserFeature.Commands.VerifyCode
{
    public class VerifyCodeHandler : IRequestHandler<VerifyCodeCommand, Result<string>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IDistributedCache _cache;
        private readonly IHttpContextAccessor _contextAccessor;
        public VerifyCodeHandler(UserManager<AppUser> userManager, IDistributedCache cache, IHttpContextAccessor contextAccessor)
        {
            _userManager = userManager;
            _cache = cache;
            _contextAccessor = contextAccessor;
        }

        public async Task<Result<string>> Handle(VerifyCodeCommand request, CancellationToken cancellationToken)
        {
            var existedUser = await _userManager.FindByEmailAsync(request.Email);
            if (existedUser is null) return Result<string>.Failure(Error.NotFound,null, ErrorType.NotFoundError);
            string clientIP = GetClientIp();
            string deviceId = GetDeviceId();
            string cacheKey = $"otp_attempts_{existedUser.Email}_{clientIP}_{deviceId}";
            var failedAttempts = await GetFailedAttempts(cacheKey);

            if (failedAttempts.BlockUntil >= DateTime.UtcNow)
            {
                return Result<string>.Failure(Error.Custom("OTP", "Too many failed attempts. Try again later."), null, ErrorType.BusinessLogicError);
            }
            if (!IsVerificationCodeValid(request.Code, existedUser))
            {
                await IncrementFailedAttempts(cacheKey);
                return Result<string>.Failure(Error.Custom("Code", "Invalid or expired verification code."), null, ErrorType.BusinessLogicError);
            }
            await ResetFailedAttempts(cacheKey);
            await UpdateUserVerificationStatusAsync(existedUser);
            return Result<string>.Success("Code verified successfully. You can now log in.", null);
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
        private string GetClientIp()
        {
            return _contextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? "UnknownIP";
        }

        private string GetDeviceId()
        {
            return _contextAccessor.HttpContext?.Request.Headers["User-Agent"].ToString() ?? "UnknownDevice";
        }
        private async Task<FailedAttemptData> GetFailedAttempts(string cacheKey)
        {
            var cachedData = await _cache.GetStringAsync(cacheKey);
            return cachedData != null ? JsonSerializer.Deserialize<FailedAttemptData>(cachedData) : new FailedAttemptData();
        }
        private async Task ResetFailedAttempts(string cacheKey)
        {
            await _cache.RemoveAsync(cacheKey);
        }
        private async Task IncrementFailedAttempts(string cacheKey)
        {
            var failedAttempts = await GetFailedAttempts(cacheKey);
            failedAttempts.Count++;

            if (failedAttempts.Count >= 5) 
            {
                failedAttempts.BlockUntil = DateTime.UtcNow.AddMinutes(10); 
            }

            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(15) 
            };

            await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(failedAttempts), options);
        }
    }
}
