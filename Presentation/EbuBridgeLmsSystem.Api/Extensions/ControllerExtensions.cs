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
            return result.SuccessReturnType switch
            {
                SuccessReturnType.NoContent => controller.NoContent(),
                SuccessReturnType.Created => controller.Created(),
                _ => controller.Ok(result)
            };
        }
        public static IResult ToApiResult<T>(this Result<T> result)
        {
            if (!result.IsSuccess)
            {
                return result.ErrorType switch
                {
                    ErrorType.NotFoundError => Results.NotFound(result),
                    ErrorType.ValidationError => Results.BadRequest(result),
                    ErrorType.UnauthorizedError => Results.Unauthorized(),
                    _ => Results.InternalServerError(result)
                };
            }
            return result.SuccessReturnType switch
            {
                SuccessReturnType.NoContent => Results.NoContent(),
                SuccessReturnType.Created => Results.Created(),
                _ => Results.Ok(result)
            };
        }

    }
}
