using EbuBridgeLmsSystem.Api.Extensions;
using EbuBridgeLmsSystem.Application.Dtos.Auth;
using EbuBridgeLmsSystem.Application.Dtos.Course;
using EbuBridgeLmsSystem.Application.Features.CourseFeature.Commands.CourseCreate;
using EbuBridgeLmsSystem.Application.Features.CourseFeature.Commands.DeleteCourse;
using FluentValidation;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EbuBridgeLmsSystem.Api.MinimalEndPoints.Admin
{
    public static class CourseEndPoints
    {
        public static void MapCourseAdminEndPointsthis (this IEndpointRouteBuilder app, string baseUrl)
        {
            app.MapPost($"{baseUrl}/Course", [Authorize(Roles = "Admin")] async ([FromForm] CourseCreateDto courseCreateDto, ISender mediator, IValidator<CourseCreateDto> _validator) =>
            {
                var validationResult = await _validator.ValidateAsync(courseCreateDto);
               
                if (!validationResult.IsValid)
                {
                    var returnedResult=Result<Unit>.Failure(null, validationResult.Errors.Select(e => e.ErrorMessage).ToList(), ErrorType.ValidationError);
                    return returnedResult.ToApiResult();
                }
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
            app.MapDelete($"{baseUrl}/Course/Delete", [Authorize(Roles = "Admin")] async([FromForm] CourseDeleteDto courseDeleteDto, IMediator mediator) =>
            {
                var courseDeleteCommand = new DeleteCourseCommand()
                {
                    Id = courseDeleteDto.Id,
                };
                var result=await mediator.Send(courseDeleteCommand);
                return result.ToApiResult();
            }).WithTags("Course");
            

                }
    }
}
