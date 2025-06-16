using EbuBridgeLmsSystem.Api.MinimalEndPoints.Admin;
using EbuBridgeLmsSystem.Api.MinimalEndPoints.ClientSide;

namespace EbuBridgeLmsSystem.Api
{
    public static class MinimalEndpointsRegister
    {
        public static void RegisterMinimalEndpoints(this IEndpointRouteBuilder app,IConfiguration configuration)
        {
            var baseAdminUrl = configuration["ApiSettings:BaseAdminUrl"];
            var clientSideUrl = configuration["ApiSettings:ClientSideUrl"];
            app.MapAuthAdminEndpoints(baseAdminUrl);
            app.MapCourseAdminEndPointsthis(baseAdminUrl);
            app.MapCityAdminEndPoints(baseAdminUrl);
            app.MapLessonMaterialEndPoints(baseAdminUrl);
            app.MapLessonAdminEndPointsthis(baseAdminUrl);
            app.MapCourseClientEndPointsthis(clientSideUrl);
            app.MapLessonStudentClientEndPoints(clientSideUrl);
            app.MapLessonStudentEndPoints(baseAdminUrl);
            app.MapRoleAdminEndpoints(baseAdminUrl);
            app.MapLanguageAdminEndpoints(baseAdminUrl);

        }
    }
}
