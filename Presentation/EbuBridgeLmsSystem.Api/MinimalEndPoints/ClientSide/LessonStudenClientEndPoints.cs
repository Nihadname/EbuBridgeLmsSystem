using EbuBridgeLmsSystem.Api.Extensions;
using EbuBridgeLmsSystem.Application.Dtos.LessonStudent;
using EbuBridgeLmsSystem.Application.Features.LessonStudentFeature.Commands.LessonStudentCreate;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EbuBridgeLmsSystem.Api.MinimalEndPoints.ClientSide
{
    public static class LessonStudenClientEndPoints
    {
        public static void MapLessonStudentClientEndPoints(this IEndpointRouteBuilder app, string baseUrl)
        {
            app.MapPost($"{baseUrl}LessonStudent", async ([FromForm] LessonStudentCreateDto LessonCreateDto, ISender mediator) =>
            {
                var LessonStudentCreateCommand = new LessonStudentCreateCommand()
                {
                    LessonId = LessonCreateDto.LessonId,
                    StudentId = LessonCreateDto.StudentId,
                };
                var result = await mediator.Send(LessonStudentCreateCommand);
                return result.ToApiResult();
            }).WithTags("LessonStudent").DisableAntiforgery();
            
        }
    }
    }
