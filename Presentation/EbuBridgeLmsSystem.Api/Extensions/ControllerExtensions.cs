using LearningManagementSystem.Core.Entities.Common;
using Microsoft.AspNetCore.Mvc;
using CommonErrorType = LearningManagementSystem.Core.Entities.Common.ErrorType;
namespace EbuBridgeLmsSystem.Api.Extensions
{
    public static class ControllerExtensions
    {
        public static IActionResult ToActionResult<T>(this ControllerBase controller, Result<T> result)
        {
            if (!result.IsSuccess)
            {
                switch (result.ErrorType)
                {
                    case CommonErrorType.NotFoundError:
                        return controller.NotFound(result);
                    case CommonErrorType.ValidationError:
                        return controller.BadRequest(result);
                    case CommonErrorType.UnauthorizedError:
                        return controller.Unauthorized(result);
                    default:
                        return controller.StatusCode(500, result);
                }
            }
            return controller.Ok(result);
        }
    }
}
