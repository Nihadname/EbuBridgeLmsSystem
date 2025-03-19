using AutoMapper;
using EbuBridgeLmsSystem.Api.Extensions;
using EbuBridgeLmsSystem.Application.Dtos.Auth;
using EbuBridgeLmsSystem.Application.Dtos.Student;
using EbuBridgeLmsSystem.Application.Features.AppUserFeature.Commands.CreateAppUserAsStudent;
using FluentValidation;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace EbuBridgeLmsSystem.Api.MinimalEndPoints.Admin
{
    public static class AuthEndpoints
    {
        public static void MapAuthAdminEndpoints(this IEndpointRouteBuilder app, string baseUrl)
        {
            app.MapPost($"{baseUrl}/Auth/RegisterForStudent", [Authorize(Roles = "Admin")] async (StudentRegistrationDto studentRegistrationDto, IMapper mapper, IMediator _mediator, IValidator<RegisterDto> validator) =>
            {
                var validationResult = await validator.ValidateAsync(studentRegistrationDto.RegisterDto);

                if (!validationResult.IsValid)
                {
                    var returnedResult = Result<Unit>.Failure(null, validationResult.Errors.Select(e => e.ErrorMessage).ToList(), ErrorType.ValidationError);
                    return returnedResult.ToApiResult();
                }
                var mappedRegisterDto = mapper.Map<CreateAppUserAsStudentCommand>(studentRegistrationDto);
                var result = await _mediator.Send(mappedRegisterDto);
                return result.ToApiResult();
            }).WithTags("Auth");
        }
    }
}