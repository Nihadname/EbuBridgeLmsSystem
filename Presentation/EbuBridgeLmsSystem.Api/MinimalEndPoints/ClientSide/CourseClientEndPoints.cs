using EbuBridgeLmsSystem.Api.Extensions;
using EbuBridgeLmsSystem.Application.Dtos.Course;
using EbuBridgeLmsSystem.Application.Features.CourseFeature.Commands.ApplyCourse;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EbuBridgeLmsSystem.Api.MinimalEndPoints.ClientSide
{
    public static class CourseClientEndPoints
    {
        public static void MapCourseClientEndPointsthis(this IEndpointRouteBuilder app, string baseUrl)
        {
            app.MapPost($"{baseUrl}Course/ApplyCourse", async ([FromForm] ApplyCourseDto ApplyCourseDto, ISender mediator) =>
            {
                var applyCourseCommand = new ApplyCourseCommand()
                {
                    CourseId = ApplyCourseDto.CourseId,
                    StudentId = ApplyCourseDto.StudentId,
                };
                var result = await mediator.Send(applyCourseCommand);
                return result.ToApiResult();
            }).WithTags("Course");
        }
    }
}
