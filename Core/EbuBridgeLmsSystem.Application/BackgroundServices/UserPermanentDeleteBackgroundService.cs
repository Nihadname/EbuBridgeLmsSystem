using EbuBridgeLmsSystem.Domain.Entities.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Runtime.InteropServices;

namespace EbuBridgeLmsSystem.Application.BackgroundServices
{
    public sealed class UserPermanentDeleteBackgroundService : BackgroundService
    {
         private readonly IServiceScopeFactory _serviceScopeFactory;

        private readonly ILogger<UserPermanentDeleteBackgroundService> _logger;
        public UserPermanentDeleteBackgroundService(ILogger<UserPermanentDeleteBackgroundService> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }



        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("user delete Service Started.");
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                        await DeleteAllSoftDeletedUsers(userManager, stoppingToken);
                    }

                    DateTime now = DateTime.Now;
                    DateTime nextExecution = now.Date.AddDays(1);
                    TimeSpan delayTime = nextExecution - now;
                    if (delayTime.TotalMilliseconds > 0)
                    {
                        await Task.Delay(delayTime, stoppingToken);
                    }

                }
                catch (TaskCanceledException)
                {
                    _logger.LogWarning("Deleting User stopped before next execution.");
                    break;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred Deleting User .");
                }
            }

        }
        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Deleting User is stopping.");
            await base.StopAsync(stoppingToken);
        }
        private async Task DeleteAllSoftDeletedUsers(UserManager<AppUser> userManager, CancellationToken stoppingToken)
        {
            var allSoftDeletedUsers = await userManager.Users.Where(s => s.IsDeleted == true && s.DeletedTime != null).ToListAsync();
            var span = CollectionsMarshal.AsSpan(allSoftDeletedUsers);
            foreach (var user in allSoftDeletedUsers)
            {
                var deletedTime = user.DeletedTime;
                var diffrenceBetweenDeletedTimeAndNow = DateTime.UtcNow.Subtract((DateTime)deletedTime).TotalDays;
                if (diffrenceBetweenDeletedTimeAndNow >= 7)
                {
                    var result = await userManager.DeleteAsync(user);
                    if (!result.Succeeded)
                    {
                        foreach (var error in result.Errors)
                        {
                            _logger.LogError($"Error deleting user {user.Id}: {error.Description}");
                        }
                    }

                }
            }
        }
    }
}