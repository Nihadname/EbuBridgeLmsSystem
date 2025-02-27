using AutoMapper;
using EbuBridgeLmsSystem.Api.Extensions;
using EbuBridgeLmsSystem.Application.Dtos.Country;
using EbuBridgeLmsSystem.Application.Features.CountryFeature.Commands.CreateCountry;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EbuBridgeLmsSystem.Api.App.Admin
{
    [Area("Admin")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;


        [HttpPost]
        public async Task<IActionResult> CreateCountry(CountryCreateDto countryCreateDto)
        {
            var mappedCommand=_mapper.Map<CreateCountryCommand>(countryCreateDto);
            var result=await _mediator.Send(mappedCommand);
            return this.ToActionResult(result);
        }
    }
}
