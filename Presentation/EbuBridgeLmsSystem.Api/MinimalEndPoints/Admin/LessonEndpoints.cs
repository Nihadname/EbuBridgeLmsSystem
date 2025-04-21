using EbuBridgeLmsSystem.Application.Dtos.Course;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EbuBridgeLmsSystem.Api.MinimalEndPoints.Admin
{
    public static class LessonEndpoints
    {
        public static void MapCourseAdminEndPointsthis(this IEndpointRouteBuilder app, string baseUrl)
        {
            app.MapPost($"{baseUrl}/Lesson", async ([FromForm] CourseCreateDto courseCreateDto, ISender mediator) =>
            {

            });
        }
    }
}
