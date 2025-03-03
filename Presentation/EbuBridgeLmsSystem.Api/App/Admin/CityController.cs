using AutoMapper;
using EbuBridgeLmsSystem.Api.Extensions;
using EbuBridgeLmsSystem.Application.Dtos.City;
using EbuBridgeLmsSystem.Application.Dtos.Country;
using EbuBridgeLmsSystem.Application.Features.CityFeature.Commands.CreateCity;
using EbuBridgeLmsSystem.Application.Features.CityFeature.Commands.UpdateCity;
using EbuBridgeLmsSystem.Application.Features.CountryFeature.Commands.UpdateCountry;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EbuBridgeLmsSystem.Api.App.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public CityController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> CreateCity(CreateCityDto createCityDto)
        {
            var CreateCityCommand=new CreateCityCommand() { 
                Name= createCityDto.Name,
                CountryId=createCityDto.CountryId,
            };  
            var result=await  _mediator.Send(CreateCityCommand);
            return this.ToActionResult(result); 
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCity(CityUpdateDto  cityUpdateDto)
        {
            var mappedCommand = _mapper.Map<UpdateCityCommand>(cityUpdateDto);
            var result = await _mediator.Send(mappedCommand);
            return this.ToActionResult(result);
        }
    }
}
