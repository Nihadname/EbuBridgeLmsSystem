using EbuBridgeLmsSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EbuBridgeLmsSystem.Application.BackgroundServices
{
    public class UserPermanentDeleteBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _services;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<UserPermanentDeleteBackgroundService> _logger;
        public UserPermanentDeleteBackgroundService(IServiceProvider services, UserManager<AppUser> userManager, ILogger<UserPermanentDeleteBackgroundService> logger)
        {
            _services = services;
            _userManager = userManager;
            _logger = logger;
        }



        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("user delete Service Started.");
            while (!stoppingToken.IsCancellationRequested)
            {

            }
            throw new NotImplementedException();
        }
        private async Task DeleteAllSoftDeletedUsers() { 
           
        }
    }
}
