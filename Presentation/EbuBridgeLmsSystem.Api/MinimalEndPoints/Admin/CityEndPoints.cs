using EbuBridgeLmsSystem.Api.Extensions;
using EbuBridgeLmsSystem.Application.Dtos.City;
using EbuBridgeLmsSystem.Application.Features.CityFeature.Commands.DeleteCity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EbuBridgeLmsSystem.Api.MinimalEndPoints.Admin
{
    public static class CityEndPoints
    {
        public static void MapCityAdminEndPoints(this IEndpointRouteBuilder app, string baseUrl)
        {
            app.MapDelete($"{baseUrl}/City/Delete", [Authorize] async ([FromBody] CityDeleteDto cityDeleteDto, IMediator _mediator) =>
            {
               
                var cityDeleteCommand = new DeleteCityCommand()
                {
                    Id = cityDeleteDto.Id,
                };
                var result = await _mediator.Send(cityDeleteCommand);
                return result.ToApiResult();
            }).WithTags("City"); 
            
        }
    }
}
