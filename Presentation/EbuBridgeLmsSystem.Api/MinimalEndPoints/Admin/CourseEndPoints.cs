using EbuBridgeLmsSystem.Api.Extensions;
using EbuBridgeLmsSystem.Application.Dtos.Course;
using EbuBridgeLmsSystem.Application.Features.CourseFeature.Commands.CourseCreate;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EbuBridgeLmsSystem.Api.MinimalEndPoints.Admin
{
    public static class CourseEndPoints
    {
        public static void MapCourseAdminEndPointsthis (this IEndpointRouteBuilder app, string baseUrl)
        {
            app.MapPost($"{baseUrl}/Course", async ([FromForm] CourseCreateDto courseCreateDto,IMediator mediator) =>
            {

                var courseCommand = new CourseCreateCommand
                {
                    Name = courseCreateDto.Name,
                    formFile = courseCreateDto.formFile,
                    Description = courseCreateDto.Description,
                    difficultyLevel=courseCreateDto.difficultyLevel,
                    DurationHours = courseCreateDto.DurationHours,
                    LanguageId = courseCreateDto.LanguageId,
                    Requirements = courseCreateDto.Requirements,
                    Price = courseCreateDto.Price,
                    StartDate = courseCreateDto.StartDate,
                    EndDate = courseCreateDto.EndDate,
                };
                var result=await mediator.Send(courseCommand);
                return result.ToApiResult();

            }).WithTags("Course").DisableAntiforgery();

        }
    }
}
