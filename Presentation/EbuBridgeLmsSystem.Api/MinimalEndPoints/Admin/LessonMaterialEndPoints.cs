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
            group.MapPost(string.Empty, [Authorize(Roles = "Admin")] async ([FromBody] LessonMaterialCreateDto lessonMaterialCreateDto, IMediator _mediator, IValidator<LessonMaterialCreateDto> validator) =>
            {
                var validationResult = await validator.ValidateAsync(lessonMaterialCreateDto);

                if (!validationResult.IsValid)
                {
                    var returnedResult = Result<Unit>.Failure(null, validationResult.Errors.Select(e => e.ErrorMessage).ToList(), ErrorType.ValidationError);
                    return returnedResult.ToApiResult();
                }
                var lessonMaterialCreateCommand = new LessonMaterialCreateCommand()
                {
                  File=lessonMaterialCreateDto.File,
                  LessonId=lessonMaterialCreateDto.LessonId,
                  Title=lessonMaterialCreateDto.Title,
                
                };
                var result = await _mediator.Send(lessonMaterialCreateCommand);
                return result.ToApiResult();
            });

        }
    }

}