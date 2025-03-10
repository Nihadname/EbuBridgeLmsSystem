using AutoMapper;
using EbuBridgeLmsSystem.Api.Extensions;
using EbuBridgeLmsSystem.Application.Dtos.Auth;
using EbuBridgeLmsSystem.Application.Features.AppUserFeature.Commands.ForgotPassword;
using EbuBridgeLmsSystem.Application.Features.AppUserFeature.Commands.GetUserAccountBack;
using EbuBridgeLmsSystem.Application.Features.AppUserFeature.Commands.Login;
using EbuBridgeLmsSystem.Application.Features.AppUserFeature.Commands.RefreshTokenCreate;
using EbuBridgeLmsSystem.Application.Features.AppUserFeature.Commands.ResetPassword;
using EbuBridgeLmsSystem.Application.Features.AppUserFeature.Commands.RevokeRefreshToken;
using EbuBridgeLmsSystem.Application.Features.AppUserFeature.Commands.SendVerificationCode;
using EbuBridgeLmsSystem.Application.Features.AppUserFeature.Commands.UserSoftDelete;
using EbuBridgeLmsSystem.Application.Features.AppUserFeature.Commands.VerifyCode;
using EbuBridgeLmsSystem.Application.Features.ProfileFeature.Queries.Profile;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace EbuBridgeLmsSystem.Api.App.ClientSide
{
    [Area("Client")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public AuthController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        [HttpPut("VerifyCode")]
        public async Task<IActionResult> VerifyCode(VerifyCodeDto verifyCodeDto)
        {
            var mappedverifyCodeCommand = _mapper.Map<VerifyCodeCommand>(verifyCodeDto);
            var result = await _mediator.Send(mappedverifyCodeCommand);
             return this.ToActionResult(result);
        }
        [HttpPut("RevokeRefreshToken")]
        [Authorize]
        public async Task<IActionResult> RevokeRefreshToken()
        {
            var refreshTokenCommand = new RevokeRefreshTokenCommand();
            var result = await _mediator.Send(refreshTokenCommand);
            return this.ToActionResult(result);
        }
        [HttpPost("SendVerificationCode")]

        public async Task<IActionResult> SendVerificationCode(SendVerificationCodeDto sendVerificationCodeDto)
        {
            var mappedSendVerificationCodeCommand = _mapper.Map<SendVerificationCodeCommand>(sendVerificationCodeDto);
            var result = await _mediator.Send(mappedSendVerificationCodeCommand);
            return this.ToActionResult(result);
        }
        [HttpGet("Profile")]
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var profileQuery = new ProfileQuery();
            var result = await _mediator.Send(profileQuery);
            return this.ToActionResult(result);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var mappedLoginCommand = _mapper.Map<LoginCommand>(loginDto);
            var result = await _mediator.Send(mappedLoginCommand);
            return this.ToActionResult(result);
        }
        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword(ResetPasswordEmailDto resetPasswordEmailDto)
        {
            var forgetPasswordCommand=_mapper.Map<ForgetPasswordCommand>(resetPasswordEmailDto);
            var result = await _mediator.Send(forgetPasswordCommand);
            return this.ToActionResult(result);
        }
        [HttpPut("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordHandleDto resetPasswordHandleDto)
        {
            var resetPasswordCommand=_mapper.Map<ResetPasswordHandleCommand>(resetPasswordHandleDto);
            var result = await _mediator.Send(resetPasswordCommand);
            return this.ToActionResult(result);
        }
        [Authorize]
        [HttpPut("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            var changePasswordCommand = _mapper.Map<ResetPasswordHandleCommand>(changePasswordDto);
            var result = await _mediator.Send(changePasswordCommand);
            return this.ToActionResult(result);
        }
        [Authorize]
        [HttpDelete("UserDelete")]
        public async Task<IActionResult> UserDelete()
        {
            var userDeleteCommand=new UserSoftDeleteCommand();
            var result = await _mediator.Send(userDeleteCommand);
            return this.ToActionResult(result);
        }
        [Authorize]
        [HttpPut("RecoverAccount")]
        public async Task<IActionResult> RecoverAccount()
        {
            var getUserAccountBackCommand = new GetUserAccountBackCommand();
            var result = await _mediator.Send(getUserAccountBackCommand);
            return this.ToActionResult(result);
        }
        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken()
        {
            RefreshTokenCommand refreshTokenCommand = new RefreshTokenCommand();
            var result = await _mediator.Send(refreshTokenCommand);
            return Ok(result);
        }
    }
}
