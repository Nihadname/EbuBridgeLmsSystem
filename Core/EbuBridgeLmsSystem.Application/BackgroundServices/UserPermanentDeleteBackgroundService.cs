using EbuBridgeLmsSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Runtime.InteropServices;

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
           var allSoftDeletedUsers=await _userManager.Users.Where(s=>s.IsDeleted==true&&s.DeletedTime!=null).ToListAsync();
            var span = CollectionsMarshal.AsSpan(allSoftDeletedUsers);
            for (int i = 0; i < allSoftDeletedUsers.Count; i++)
            {
                var deletedTime = span[i].DeletedTime;
                var diffrenceBetweenDeletedTimeAndNow = DateTime.UtcNow.Subtract((DateTime)deletedTime).Days;
                if (diffrenceBetweenDeletedTimeAndNow >= 7)
                {
                  var result=  await _userManager.DeleteAsync(span[i]);
                    if (!result.Succeeded)
                    {
                        _logger.LogError(result.ToString(), "Error occurred while creating fees.");
                    }
                   
                }
            }
        }
    }
}
