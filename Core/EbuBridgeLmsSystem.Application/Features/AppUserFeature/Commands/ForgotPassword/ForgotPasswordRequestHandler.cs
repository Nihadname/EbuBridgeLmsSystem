using EbuBridgeLmsSystem.Application.Dtos.Auth;
using EbuBridgeLmsSystem.Application.Interfaces;
using EbuBridgeLmsSystem.Domain.Entities;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Features.AppUserFeature.Commands.ForgotPassword
{
    public class ForgotPasswordRequestHandler : IRequestHandler<ForgotPasswordCommand, Result<Unit>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IEmailService _emailService;

        public ForgotPasswordRequestHandler(UserManager<AppUser> userManager, IHttpContextAccessor contextAccessor, IEmailService emailService)
        {
            _userManager = userManager;
            _contextAccessor = contextAccessor;
            _emailService = emailService;
        }

        public async Task<Result<Unit>> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var userResult = await GetUserByEmailAsync(request.Email);
            if (!userResult.IsSuccess) return Result<Unit>.Failure(userResult.ErrorKey, userResult.Message, userResult.Errors, (ErrorType)userResult.ErrorType);
            var token = await _userManager.GeneratePasswordResetTokenAsync(userResult.Data);
            if (string.IsNullOrWhiteSpace(token))
              return  Result<Unit>.Failure("Error", "Null result", null, ErrorType.SystemError);
            request.Token = token;
            var httpRequest = _contextAccessor.HttpContext.Request;
            var baseUrl = $"{httpRequest.Scheme}://{httpRequest.Host}{httpRequest.PathBase}";
            var resetPasswordEndpoint= $"{baseUrl}/api/reset-password";
           var bodyResult= await GetEmailBody(token, resetPasswordEndpoint);
          if(!bodyResult.IsSuccess)
                return Result<Unit>.Failure(userResult.ErrorKey, userResult.Message, userResult.Errors, (ErrorType)userResult.ErrorType);
            await _emailService.SendEmailAsync(userResult.Data.Email, "Reset Password", bodyResult.Data, true);
            return Result<Unit>.Success(Unit.Value);
        }

        private async Task<Result<AppUser>> GetUserByEmailAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return Result<AppUser>.Failure(null, "Email is required.", null, ErrorType.ValidationError);
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return Result<AppUser>.Failure(null, "User not found.", null, ErrorType.NotFoundError);
            }
            return Result<AppUser>.Success(user);
        }
        private async Task<Result<string>> GetEmailBody(string token, string apiEndpoint)
        {
            if (string.IsNullOrWhiteSpace(token) || string.IsNullOrWhiteSpace(apiEndpoint))
            {
                return Result<string>.Failure(null, "arguments sent null", null, ErrorType.NotFoundError);
            }
            string body = @$"
<html>
  <head>
    <meta charset='utf-8'>
    <title>Password Reset Request</title>
  </head>
  <body>
    <h2>Password Reset Request</h2>
    <p>You requested a password reset.</p>
    <p>Use this token in your password reset API endpoint: <strong>{token}</strong>.</p>
    <p>
      To reset your password, send a POST request to 
      <a href='{apiEndpoint}'>{apiEndpoint}</a> 
      with the token and your new password.
    </p>
    <p>If you did not request a password reset, please ignore this email.</p>
  </body>
</html>";
            return Result<string>.Success(body);
        }
    }
}
