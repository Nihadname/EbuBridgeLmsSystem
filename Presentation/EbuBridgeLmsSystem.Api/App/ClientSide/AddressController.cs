using AutoMapper;
using EbuBridgeLmsSystem.Api.Extensions;
using EbuBridgeLmsSystem.Application.Dtos.Address;
using EbuBridgeLmsSystem.Application.Features.AddressFeature.Commands.AddressCreate;
using EbuBridgeLmsSystem.Application.Features.AddressFeature.Commands.DeleteAddress;
using EbuBridgeLmsSystem.Application.Features.AddressFeature.Commands.UpdateAdress;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EbuBridgeLmsSystem.Api.App.ClientSide
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly ISender _mediator;
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
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateAddress(UpdateAddressDto updateAddressDto)
        {
            if (updateAddressDto == null)
            {
                return BadRequest("Invalid request data.");
            }
            var mappedAddressCommand = _mapper.Map<UpdateAddressCommand>(updateAddressDto);
            var result = await _mediator.Send(mappedAddressCommand);
            return this.ToActionResult(result);
        }
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteAddress(DeleteAddressDto deleteAddressDto)
        {
            var mappedAddressCommand = _mapper.Map<DeleteAddressCommand>(deleteAddressDto);
            var result = await _mediator.Send(mappedAddressCommand);
            return this.ToActionResult(result);
        }
    }
}
