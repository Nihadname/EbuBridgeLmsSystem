using EbuBridgeLmsSystem.Domain.Entities;
using EbuBridgeLmsSystem.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Text.Json;

namespace LearningManagementSystem.API.Middlewares
{
    //public class FeeProcessingMiddleware
    //{
    //    private readonly RequestDelegate _next;
    //    private readonly IServiceProvider _serviceProvider;
    //    private readonly HashSet<string> _excludedPaths;
    //    private readonly ILogger<FeeProcessingMiddleware> _logger;

    //    public FeeProcessingMiddleware(RequestDelegate next, IServiceProvider serviceProvider, ILogger<FeeProcessingMiddleware> logger)
    //    {
    //        _next = next;
    //        _serviceProvider = serviceProvider;
    //        _excludedPaths = new HashSet<string>(new[]
    //        {
    //            "/api/fee/processpayment",
    //            "/api/fee/uploadimageofbanktransfer",
    //            "/api/fee/getallfeesforuser"
    //        }, StringComparer.OrdinalIgnoreCase);
    //        _logger = logger;
    //    }

    //    public async Task InvokeAsync(HttpContext context)
    //    {
    //        var requestPath = context.Request.Path.Value;
    //        if (_excludedPaths.Any(path => requestPath.StartsWith(path, StringComparison.OrdinalIgnoreCase)))
    //        {
    //            await _next(context);
    //            return;
    //        }

    //        var endpoint = context.GetEndpoint();
    //        var hasAuthorizeAttribute = endpoint?.Metadata.Any(meta => meta is AuthorizeAttribute) ?? false;
    //        if (!hasAuthorizeAttribute)
    //        {
    //            await _next(context);
    //            return;
    //        }

    //        if (context.User.Identity?.IsAuthenticated == true)
    //        {
    //            var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    //            var userManager = context.RequestServices.GetService<UserManager<AppUser>>();
    //            var existedUser = await userManager.FindByIdAsync(userId);
    //            if (existedUser is null || !(await userManager.GetRolesAsync(existedUser)).Contains(RolesEnum.Student.ToString()))
    //            {
    //                await _next(context);
    //                return;
    //            }

    //            var feeService = context.RequestServices.GetService<IFeeService>();

    //            if (!string.IsNullOrEmpty(userId) && feeService != null)
    //            {
    //                var isFeePaid = await feeService.IsFeePaid(userId);
    //                if (!isFeePaid.Data)
    //                {
    //                    var errorResponse = new
    //                    {
    //                        StatusCode = StatusCodes.Status403Forbidden,
    //                        Message = "Access denied. Fees are unpaid.",
    //                        Errors = new { Reason = "User has not paid the required fees." }
    //                    };

    //                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
    //                    context.Response.ContentType = "application/json";
    //                    await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
    //                    return;
    //                }
    //            }
    //        }

    //        await _next(context);
    //    }
    //}
}
