using EbuBridgeLmsSystem.Api.Extensions;
using EbuBridgeLmsSystem.Application.Dtos.Language;
using EbuBridgeLmsSystem.Application.Features.LanguageFeature.Commands.LanguageCreate;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EbuBridgeLmsSystem.Api.MinimalEndPoints.Admin;

public static class LanguageEndPoints
{
    public static void MapLanguageAdminEndpoints(this IEndpointRouteBuilder app, string baseUrl)
    {
        app.MapPost($"{baseUrl}/Language", async ([FromForm] LanguageCreateDto LanguageCreateDto, ISender mediator) =>
        {
            var newLanguageCreateCommand = new LanguageCreateCommand()
            {
Name = LanguageCreateDto.Name,
            };
         var result=   await mediator.Send(newLanguageCreateCommand);
         return result.ToApiResult();
        }).WithTags("Language");;
    }
}