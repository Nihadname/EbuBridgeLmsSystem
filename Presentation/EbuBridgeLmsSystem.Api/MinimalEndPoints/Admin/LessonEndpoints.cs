using EbuBridgeLmsSystem.Api.Extensions;
using EbuBridgeLmsSystem.Application.Dtos.Lesson;
using EbuBridgeLmsSystem.Application.Features.LessonFeature.Commands.LessonCreate;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EbuBridgeLmsSystem.Api.MinimalEndPoints.Admin
{
    public static class LessonEndpoints
    {
        public static void MapLessonAdminEndPointsthis(this IEndpointRouteBuilder app, string baseUrl)
        {
            app.MapPost($"{baseUrl}/Lesson", async ( LessonCreateDto LessonCreateDto, ISender mediator) =>
            {
                var newLessonCreateCommand = new LessonCreateCommand()
                {
                    Status=LessonCreateDto.Status,
                    CourseId=LessonCreateDto.CourseId,
                    Description=LessonCreateDto.Description,
                    GradingPolicy=LessonCreateDto.GradingPolicy,
                    LessonType=LessonCreateDto.LessonType,
                    Title=LessonCreateDto.Title,
                };
                var result = await mediator.Send(newLessonCreateCommand);
                return result.ToApiResult();
            }).WithTags("Lesson");
        }
    }
}
