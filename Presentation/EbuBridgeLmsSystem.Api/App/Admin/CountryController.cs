using AutoMapper;
using EbuBridgeLmsSystem.Api.Extensions;
using EbuBridgeLmsSystem.Application.Dtos.Country;
using EbuBridgeLmsSystem.Application.Features.CountryFeature.Commands.CreateCountry;
using EbuBridgeLmsSystem.Application.Features.CountryFeature.Commands.DeleteCountry;
using EbuBridgeLmsSystem.Application.Features.CountryFeature.Commands.UpdateCountry;
using EbuBridgeLmsSystem.Application.Features.CountryFeature.Queries.GetAllCountries;
using EbuBridgeLmsSystem.Application.Features.CountryFeature.Queries.GetByIdCountry;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        public CountryController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCountry(CountryCreateDto countryCreateDto)
        {
            var mappedCommand=_mapper.Map<CreateCountryCommand>(countryCreateDto);
            var result=await _mediator.Send(mappedCommand);
            return this.ToActionResult(result);
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCountry(CountryUpdateDto countryUpdateDto)
        {
           var mappedCommand=_mapper.Map<UpdateCountryCommand>(countryUpdateDto);
            var result = await _mediator.Send(mappedCommand);
            return this.ToActionResult(result);
        }
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCountry(CountryDeleteDto countryDeleteDto)
        {
            var mappedCommand = _mapper.Map<DeleteCountryCommand>(countryDeleteDto);
            var result = await _mediator.Send(mappedCommand);
            return this.ToActionResult(result);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async  Task<IActionResult> GetAll(string? cursor = null, string? searchQuery = null,  int limit = 4)
        {
            var getAllCountriesQuery = new GetAllCountriesQuery()
            {
                Cursor = cursor,
                searchQuery = searchQuery,
                Limit = limit
            };
            var result = await _mediator.Send(getAllCountriesQuery);
            return this.ToActionResult(result);
        }
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById(Guid id)
        {
            if(id == Guid.Empty)
            {
                return this.BadRequest();
            }
            var getByIdCountryQuery = new GetByIdCountryQuery()
            {
                Id = id
            };
            var result = await _mediator.Send(getByIdCountryQuery);
            return this.ToActionResult(result);
        }

    }
}
