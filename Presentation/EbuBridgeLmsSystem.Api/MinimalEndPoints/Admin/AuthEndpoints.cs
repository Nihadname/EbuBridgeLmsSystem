using AutoMapper;
using EbuBridgeLmsSystem.Api.Extensions;
using EbuBridgeLmsSystem.Application.Dtos.Auth;
using EbuBridgeLmsSystem.Application.Dtos.Student;
using EbuBridgeLmsSystem.Application.Features.AppUserFeature.Commands.CreateAppUserAsStudent;
using FluentValidation;
using MediatR;

namespace EbuBridgeLmsSystem.Api.MinimalEndPoints.Admin
{
    public static class AuthEndpoints
    {
        public static void MapAuthAdminEndpoints(this IEndpointRouteBuilder app, string baseUrl)
        {
            app.MapPost($"{baseUrl}/Auth/RegisterForStudent", async (StudentRegistrationDto studentRegistrationDto, IMapper mapper, ISender _mediator, IValidator<RegisterDto> validator) =>
            {
                
                var mappedRegisterDto = mapper.Map<CreateAppUserAsStudentCommand>(studentRegistrationDto);
                var result = await _mediator.Send(mappedRegisterDto);
                return result.ToApiResult();
            }).WithTags("Auth");
        }
    }
}