using AutoMapper;
using EbuBridgeLmsSystem.Application.Dtos.Student;
using EbuBridgeLmsSystem.Application.Features.AppUserFeature.Commands.CreateAppUserAsStudent;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> RegisterForStudent(StudentRegistrationDto studentRegistrationDto)
        {
            var mappedRegisterDto=_mapper.Map<CreateAppUserAsStudentCommand>(studentRegistrationDto);
            var result = await _mediator.Send(mappedRegisterDto);
           return Ok(result);
        }
    }
}
