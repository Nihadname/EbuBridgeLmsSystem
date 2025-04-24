using EbuBridgeLmsSystem.Api.Extensions;
using EbuBridgeLmsSystem.Application.Dtos.LessonStudent;
using EbuBridgeLmsSystem.Application.Features.LessonStudentFeature.Commands.LessonStudentApproval;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EbuBridgeLmsSystem.Api.MinimalEndPoints.Admin
{
    public static class LessonStudentEndPoints
    {
        public static void MapLessonStudentEndPoints(this IEndpointRouteBuilder app, string baseUrl)
        {
            app.MapPatch($"{baseUrl}LessonStudent/ApproveLessonStudentRequest", async ([FromForm] LessonStudentApproveRequestDto LessonStudentApproveRequestDto, ISender mediator) =>
            {
                var LessonStudentApproveRequestCommand = new LessonStudentApprovalCommand()
                {
                    LessonStudentId = LessonStudentApproveRequestDto.Id
                };
                var result = await mediator.Send(LessonStudentApproveRequestCommand);
                return result.ToApiResult();
            }).WithTags("LessonStudent").DisableAntiforgery();

        }
    }
}
