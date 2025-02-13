using AutoMapper;
using EbuBridgeLmsSystem.Application.Dtos.Auth;
using EbuBridgeLmsSystem.Application.Features.AppUser.Commands.CreateAppUserAsStudent;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace EbuBridgeLmsSystem.Api.App.Admin
{
    [Area("Admin")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly  IMediator _mediator;
        private readonly IMapper _mapper;
        public AuthController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> RegisterForStudent(RegisterDto registerDto)
        {
            var mappedRegisterDto=_mapper.Map<CreateAppUserAsStudentCommand>(registerDto);
            var result = await _mediator.Send(mappedRegisterDto);
           return Ok(result);
        }
    }
}
