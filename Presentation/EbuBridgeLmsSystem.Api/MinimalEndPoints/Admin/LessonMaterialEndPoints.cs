using EbuBridgeLmsSystem.Api.Extensions;
using EbuBridgeLmsSystem.Application.Dtos.City;
using EbuBridgeLmsSystem.Application.Dtos.Course;
using EbuBridgeLmsSystem.Application.Dtos.LessonMaterial;
using EbuBridgeLmsSystem.Application.Features.LessonMaterialFeature.Commands.LessonMaterialCreate;
using FluentValidation;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EbuBridgeLmsSystem.Api.MinimalEndPoints.Admin
{
    public static class LessonMaterialEndPoints
    {
        public static void MapLessonMaterialEndPoints(this IEndpointRouteBuilder app, string baseUrl)
        {
            RouteGroupBuilder group = app.MapGroup($"{baseUrl}/LessonMaterial").WithTags("LessonMaterial").RequireAuthorization();
            group.MapPost(string.Empty, [Authorize(Roles = "Admin")] async ([FromBody] LessonMaterialCreateDto lessonMaterialCreateDto, ISender _mediator, IValidator<LessonMaterialCreateDto> validator) =>
            {
                
                var lessonMaterialCreateCommand = new LessonMaterialCreateCommand()
                {
                  File=lessonMaterialCreateDto.File,
                  LessonUnitId=lessonMaterialCreateDto.LessonUnitId,
                  Title=lessonMaterialCreateDto.Title,
                
                };
                var result = await _mediator.Send(lessonMaterialCreateCommand);
                return result.ToApiResult();
            });

        }
    }

}