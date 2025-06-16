using EbuBridgeLmsSystem.Api.Extensions;
using EbuBridgeLmsSystem.Application.Dtos.Course;
using EbuBridgeLmsSystem.Application.Features.CourseFeature.Commands.ApproveStudentCourseRequest;
using EbuBridgeLmsSystem.Application.Features.CourseFeature.Commands.CourseCreate;
using EbuBridgeLmsSystem.Application.Features.CourseFeature.Commands.DeleteCourse;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EbuBridgeLmsSystem.Api.MinimalEndPoints.Admin
{
    public static class CourseEndPoints
    {
        public static void MapCourseAdminEndPointsthis (this IEndpointRouteBuilder app, string baseUrl)
        {
            app.MapPost($"{baseUrl}/Course",  async ([FromForm] CourseCreateDto courseCreateDto, ISender mediator) =>
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
                };
                var result=await mediator.Send(courseCommand);
                return result.ToApiResult();

            }).WithTags("Course");
            app.MapDelete($"{baseUrl}/Course/Delete", async(CourseDeleteDto courseDeleteDto, IMediator mediator) =>
            {
                var courseDeleteCommand = new DeleteCourseCommand()
                {
                    Id = courseDeleteDto.Id,
                };
                var result=await mediator.Send(courseDeleteCommand);
                return result.ToApiResult();
            }).WithTags("Course");

            app.MapPatch($"{baseUrl}/Course/ApproveStudentCourseRequest",  async (ApproveStudentCourseRequestDto ApproveStudentCourseRequestDto,  ISender mediator) =>
            {
                var newApproveStudentCourseRequestCommand = new ApproveStudentCourseRequestCommand()
                {
                    Id=ApproveStudentCourseRequestDto.Id
                };
                var result = await mediator.Send(newApproveStudentCourseRequestCommand);
                return result.ToApiResult();
            }).WithTags("Course");
           
            }
        
    }
}
