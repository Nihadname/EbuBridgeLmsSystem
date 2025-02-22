using AutoMapper;
using EbuBridgeLmsSystem.Application.Dtos.Auth;
using EbuBridgeLmsSystem.Application.Features.AppUserFeature.Commands.Login;
using EbuBridgeLmsSystem.Application.Features.AppUserFeature.Commands.RevokeRefreshToken;
using EbuBridgeLmsSystem.Application.Features.AppUserFeature.Commands.SendVerificationCode;
using EbuBridgeLmsSystem.Application.Features.AppUserFeature.Commands.VerifyCode;
using EbuBridgeLmsSystem.Application.Features.ProfileFeature.Queries.Profile;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
            var mappedverifyCodeDto = _mapper.Map<VerifyCodeCommand>(verifyCodeDto);
            var result = await _mediator.Send(mappedverifyCodeDto);
            return Ok(result);
        }
        [HttpPut("RevokeRefreshToken")]
        [Authorize]
        public async Task<IActionResult> RevokeRefreshToken()
        {
            var refreshTokenCommand = new RevokeRefreshTokenCommand();
            var result = await _mediator.Send(refreshTokenCommand);
            return Ok(result);
        }
        [HttpGet("SendVerificationCode")]

        public async Task<IActionResult> SendVerificationCode(SendVerificationCodeDto sendVerificationCodeDto)
        {
            var mappedSendVerificationCodeDto = _mapper.Map<SendVerificationCodeCommand>(sendVerificationCodeDto);
            var result = await _mediator.Send(mappedSendVerificationCodeDto);
            return Ok(result);
        }
        [HttpGet("Profile")]
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var profileQuery = new ProfileQuery();
            var result = await _mediator.Send(profileQuery);
            return Ok(result);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var mappedLoginDto = _mapper.Map<LoginCommand>(loginDto);
            var result = await _mediator.Send(mappedLoginDto);
            return Ok(result);
        }
    }
}
