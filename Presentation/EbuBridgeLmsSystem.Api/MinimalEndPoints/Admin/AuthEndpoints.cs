using AutoMapper;
using EbuBridgeLmsSystem.Api.Extensions;
using EbuBridgeLmsSystem.Application.Dtos.Student;
using EbuBridgeLmsSystem.Application.Features.AppUserFeature.Commands.CreateAppUserAsStudent;
using EbuBridgeLmsSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace EbuBridgeLmsSystem.Api.MinimalEndPoints.Admin
{
    public static class AuthEndpoints
    {
        public static void MapAuthAdminEndpoints(this IEndpointRouteBuilder app, string baseUrl)
        {
            app.MapPost($"{baseUrl}/Auth/RegisterForStudent", [Authorize] async (StudentRegistrationDto studentRegistrationDto, IMapper mapper, IMediator _mediator) =>
            {
                var mappedRegisterDto = mapper.Map<CreateAppUserAsStudentCommand>(studentRegistrationDto);
                var result = await _mediator.Send(mappedRegisterDto);
                return result.ToApiResult();
            });
        }
    }
}