using AutoMapper;
using EbuBridgeLmsSystem.Api.App.Common;
using EbuBridgeLmsSystem.Api.Extensions;
using EbuBridgeLmsSystem.Application.Features.AddressFeature.Queries.GetAllAddresses;
using EbuBridgeLmsSystem.Application.Features.CityFeature.Queries.GetAllCities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EbuBridgeLmsSystem.Api.App.Admin
{
    [Area("Admin")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class AddressController : BaseAdminController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public AddressController(IMapper mapper, ISender mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }
        [HttpGet]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetAll(string? cursor = null, string? searchQuery = null, int limit = 4)
        {
            var  getAllAddressQuery = new GetAllAddressQuery()
            {
                Cursor = cursor,
                searchQuery = searchQuery,
                Limit = limit
            };
            var result = await _mediator.Send(getAllAddressQuery);
            return this.ToActionResult(result);
        }
    }
}
