//using LearningManagementSystem.Application.Helpers.Enums;
//using LearningManagementSystem.Application.Interfaces;
//using LearningManagementSystem.Core.Entities;
//using LearningManagementSystem.Core.Entities.Common;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Identity;

//namespace LearningManagementSystem.Api.Middlewares
//{
//    public class FeeProcessingMiddleware
//    {
//        private readonly RequestDelegate _next;
//        private readonly IServiceProvider _serviceProvider;
//        private readonly HashSet<string> _excludedPaths;
//        private readonly ILogger<FeeProcessingMiddleware> _logger;
//        public FeeProcessingMiddleware(RequestDelegate next, IServiceProvider serviceProvider, ILogger<FeeProcessingMiddleware> logger)
//        {
//            _next = next;
//            _serviceProvider = serviceProvider;
//            _excludedPaths = new HashSet<string>(new[] { "/api/Fee/ProcessPayment", "/api/Fee/UploadImageOfBankTransfer", "/api/Fee/GetAllFeesForUser" }, StringComparer.OrdinalIgnoreCase);
//            _logger = logger;
//        }
//        public async Task InvokeAsync(HttpContext context)
//        {
//            var request = context.Request.Path.Value;
//            if (_excludedPaths.Any(path => request.ToLower().StartsWith(path.ToLower())))
//            {
//                await _next(context);
//                return;
//            }
//            var endpoint = context.GetEndpoint();
//            var hasAuthorizeAttribute = endpoint?.Metadata.Any(meta => meta is AuthorizeAttribute) ?? false;
//            if (!hasAuthorizeAttribute)
//            {
//                await _next(context);
//                return;
//            }


//            if (context.User.Identity?.IsAuthenticated == true)
//            {
//                var userId = context.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
//                var userManager = context.RequestServices.GetService<UserManager<AppUser>>();
//                var existedUser= await userManager.FindByIdAsync(userId);
//                if(existedUser is null||!(await userManager.GetRolesAsync(existedUser)).Contains(RolesEnum.Student.ToString()))
//                {
//                    await _next(context);
//                    return;
//                }
//                var feeService = context.RequestServices.GetService<IFeeService>();

//                if (!string.IsNullOrEmpty(userId) && feeService != null)
//                {
//                    Result<bool> isFeePaid = await feeService.IsFeePaid(userId);
//                    if (!isFeePaid.Data)
//                    {
//                        var errorResponse = new
//                        {
//                            StatusCode = StatusCodes.Status403Forbidden,
//                            Message = "Access denied. Fees are unpaid.",
//                            Errors = new { Reason = "User has not paid the required fees." }
//                        };

//                        context.Response.StatusCode = StatusCodes.Status403Forbidden;
//                        context.Response.ContentType = "application/json";
//                        await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(errorResponse));
//                        return;
//                    }
//                }
//            }

//            await _next(context);
//        }

//    }
//}
