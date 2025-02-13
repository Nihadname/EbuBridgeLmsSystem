using EbuBridgeLmsSystem.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace EbuBridgeLmsSystem.Persistance.AuthHandler.HttpAuth
{
    public class AppUserResolver : IAppUserResolver
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<AppUser> _userManager;

        public AppUserResolver(IHttpContextAccessor contextAccessor, UserManager<AppUser> userManager)
        {
            _contextAccessor = contextAccessor;
            _userManager = userManager;
        }

        public string UserId => _contextAccessor.HttpContext?.User
        .FindFirst(ClaimTypes.NameIdentifier)?.Value;

        public string UserName => _contextAccessor.HttpContext?.User
        .FindFirst(ClaimTypes.Name)?.Value;

        public bool IsAuthenticated => _contextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;

        public async Task<AppUser> GetCurrentUserAsync()
        {
            if (!IsAuthenticated)
                return null;
            if (string.IsNullOrEmpty(UserId))
                return null;
            var currentUserInTheSystem=await _userManager.FindByIdAsync(UserId);
            if (currentUserInTheSystem != null)
                return currentUserInTheSystem;
            return null;

        }
    }
}
