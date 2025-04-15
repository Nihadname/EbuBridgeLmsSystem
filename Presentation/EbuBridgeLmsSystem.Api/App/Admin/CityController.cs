using AutoMapper;
using EbuBridgeLmsSystem.Api.Extensions;
using EbuBridgeLmsSystem.Application.Dtos.City;
using EbuBridgeLmsSystem.Application.Features.CityFeature.Commands.CreateCity;
using EbuBridgeLmsSystem.Application.Features.CityFeature.Commands.UpdateCity;
using EbuBridgeLmsSystem.Application.Features.CityFeature.Queries.GetAllCities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EbuBridgeLmsSystem.Application.Features.CityFeature.Queries.GetByIdCity;
using EbuBridgeLmsSystem.Api.App.Common;

namespace EbuBridgeLmsSystem.Api.App.Admin
{
    [Area("Admin")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class CityController : BaseAdminController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;
        public CityController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCity(CreateCityDto createCityDto)
        {
            var CreateCityCommand = new CreateCityCommand()
            {
                Name = createCityDto.Name,
                CountryId = createCityDto.CountryId,
            };
            var result = await _mediator.Send(CreateCityCommand);
            return this.ToActionResult(result);
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCity(CityUpdateDto cityUpdateDto)
        {
            var mappedCommand = _mapper.Map<UpdateCityCommand>(cityUpdateDto);
            var result = await _mediator.Send(mappedCommand);
            return this.ToActionResult(result);
        }
        [HttpGet("GetAllWithPaganation")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll(string? cursor = null, string? searchQuery = null, int limit = 4)
        {
            var cityGetAllQuery = new GetAllCitiesQuery()
            {
                Cursor = cursor,
                searchQuery = searchQuery,
                Limit = limit
            };
            var result = await _mediator.Send(cityGetAllQuery);
            return this.ToActionResult(result);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async  Task<IActionResult> Get(Guid id)
        {
            if (id == Guid.Empty)
            {
                return this.BadRequest();
            }
            var cityGetByIdQuery = new GetByIdCityQuery()
            {
                Id = id
            };
            var result = await _mediator.Send(cityGetByIdQuery);
            return this.ToActionResult(result);
        }
    }
}
