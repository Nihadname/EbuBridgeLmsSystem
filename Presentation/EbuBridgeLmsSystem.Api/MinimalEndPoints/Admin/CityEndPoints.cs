using EbuBridgeLmsSystem.Api.Extensions;
using EbuBridgeLmsSystem.Application.Dtos.City;
using EbuBridgeLmsSystem.Application.Dtos.Course;
using EbuBridgeLmsSystem.Application.Features.CityFeature.Commands.DeleteCity;
using FluentValidation;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EbuBridgeLmsSystem.Api.MinimalEndPoints.Admin
{
    public static class CityEndPoints
    {
        public static void MapCityAdminEndPoints(this IEndpointRouteBuilder app, string baseUrl)
        {
            RouteGroupBuilder group = app.MapGroup($"{baseUrl}/City").WithTags("City").RequireAuthorization();
            group.MapDelete(string.Empty, [Authorize(Roles ="Admin")] async ([FromBody] CityDeleteDto cityDeleteDto, IMediator _mediator,IValidator< CityDeleteDto> validator) =>
            {
                var validationResult = await validator.ValidateAsync(cityDeleteDto);

                if (!validationResult.IsValid)
                {
                    var returnedResult = Result<Unit>.Failure(null, validationResult.Errors.Select(e => e.ErrorMessage).ToList(), ErrorType.ValidationError);
                    return returnedResult.ToApiResult();
                }
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
