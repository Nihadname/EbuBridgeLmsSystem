using EbuBridgeLmsSystem.Application.Dtos.Auth;
using EbuBridgeLmsSystem.Application.Helpers.Extensions.Auth;
using EbuBridgeLmsSystem.Application.Interfaces;
using EbuBridgeLmsSystem.Domain.Entities;
using EbuBridgeLmsSystem.Domain.Entities.Common;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace EbuBridgeLmsSystem.Application.Features.AppUserFeature.Commands.ForgotPassword
{
    public class ForgotPasswordRequestHandler : IRequestHandler<ForgetPasswordCommand, Result<Unit>>
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

        public async Task<Result<Unit>> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
        {
            var authEmailGetDto=new AuthGetEmailDto {Email= request.Email };
            var userResult = await _userManager.GetUserByEmailAsync(request.Email);
            if (!userResult.IsSuccess) return Result<Unit>.Failure(userResult.Error, userResult.Errors, (ErrorType)userResult.ErrorType);
            var token = await _userManager.GeneratePasswordResetTokenAsync(userResult.Data);
            if (string.IsNullOrWhiteSpace(token))
              return  Result<Unit>.Failure(Error.ValidationFailed, null, ErrorType.SystemError);
            request.Token = token;
            var httpRequest = _contextAccessor.HttpContext.Request;
            var baseUrl = $"{httpRequest.Scheme}://{httpRequest.Host}{httpRequest.PathBase}";
            var resetPasswordEndpoint= $"{baseUrl}/api/reset-password";
            var authGetEmailBodyDto=new AuthGetEmailBodyDto { Token=token,ApiEndpoint=resetPasswordEndpoint };
           var bodyResult=  GetEmailBody(authGetEmailBodyDto);
          if(!bodyResult.IsSuccess)
                return Result<Unit>.Failure(userResult.Error, userResult.Errors, (ErrorType)userResult.ErrorType);
            await _emailService.SendEmailAsync(userResult.Data.Email, "Reset Password", bodyResult.Data, true);
            return Result<Unit>.Success(Unit.Value,SuccessReturnType.NoContent);
        }

        
        private Result<string> GetEmailBody(AuthGetEmailBodyDto authGetEmailBodyDto)
        {
            string body= @$"
<html>
  <head>
    <meta charset='utf-8'>
    <title>Password Reset Request</title>
  </head>
  <body>
    <h2>Password Reset Request</h2>
    <p>You requested a password reset.</p>
    <p>Use this token in your password reset API endpoint: <strong>{authGetEmailBodyDto.Token}</strong>.</p>
    <p>
      To reset your password, send a POST request to 
      <a href='{authGetEmailBodyDto.ApiEndpoint}'>{authGetEmailBodyDto.ApiEndpoint}</a> 
      with the token and your new password.
    </p>
    <p>If you did not request a password reset, please ignore this email.</p>
  </body>
</html>";
            return Result<string>.Success(body, null);
        }
    }
}
