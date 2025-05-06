using EbuBridgeLmsSystem.Api.Extensions;
using EbuBridgeLmsSystem.Application.Features.RoleFeature.Comands.RoleCreate;
using MediatR;

namespace EbuBridgeLmsSystem.Api.MinimalEndPoints.Admin
{
    public static class AdminRoleEndPoints
    {
        public static void MapRoleAdminEndpoints(this IEndpointRouteBuilder app, string baseUrl)
        {
            app.MapPost($"{baseUrl}/Role/Create", async (RoleCreateCommand roleCreateCommand,ISender mediator) =>
            {
                var result =await mediator.Send(roleCreateCommand);
                return result.ToApiResult();
            }).WithTags("Role");
        }
    }
}
