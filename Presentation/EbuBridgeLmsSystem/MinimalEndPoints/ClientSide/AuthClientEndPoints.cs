using EbuBridgeLmsSystem.Application.Features.AppUserFeature.Commands.CreateAppUserAsSaasStudent;
using MediatR;

namespace EbuBridgeLmsSystem.LmsAiApi.MinimalEndPoints.ClientSide
{
    public static class AuthClientEndPoints
    {
        public static void MapAuthClientEndPoints(this IEndpointRouteBuilder app, string baseUrl)
        {
            app.MapPost($"{baseUrl}/Auth/Register", async (CreateAppUserAsSaasStudentCommand CreateAppUserAsSaasStudentCommand, ISender mediator) =>
            {
                var result = await mediator.Send(CreateAppUserAsSaasStudentCommand);
                return result;
            }).WithTags("Auth");
        }
    }
}
