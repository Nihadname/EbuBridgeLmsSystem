using AutoMapper;
using EbuBridgeLmsSystem.Api.Extensions;
using EbuBridgeLmsSystem.Application.Dtos.Address;
using EbuBridgeLmsSystem.Application.Features.AddressFeature.Commands.AddressCreate;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EbuBridgeLmsSystem.Api.App.ClientSide
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AddressController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }
        [HttpPost("CreateAddress")]
        [Authorize]
        public async Task<IActionResult> CreateAddress(AddressCreateDto addressCreateDto)
        {
            var mappedAddressCommand=_mapper.Map<AddressCreateCommand>(addressCreateDto);
            var result=await _mediator.Send(mappedAddressCommand);
            return this.ToActionResult(result);
        }
    }
}
