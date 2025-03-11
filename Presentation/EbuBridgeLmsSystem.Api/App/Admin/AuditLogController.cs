using EbuBridgeLmsSystem.Api.Extensions;
using EbuBridgeLmsSystem.Application.Features.AddressFeature.Queries.GetAllAddresses;
using EbuBridgeLmsSystem.Application.Features.AuditLogFeature.Query.GetAllLogs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EbuBridgeLmsSystem.Api.App.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditLogController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuditLogController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll(string? cursor = null, string? TableNameSearchQuery = null, string? ActionSearchQuery = null,string? UserId=null, int limit = 4)
        {
            var getAllAddressQuery = new GetAllLogsQuery()
            {
                Cursor = cursor,
                TableNameSearchQuery=TableNameSearchQuery,
                ActionSearchQuery=ActionSearchQuery,
                UserId=UserId,
                Limit = limit
            };
            var result = await _mediator.Send(getAllAddressQuery);
            return this.ToActionResult(result);
        }
    }
}
