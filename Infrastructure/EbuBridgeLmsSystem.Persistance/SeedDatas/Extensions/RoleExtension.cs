using EbuBridgeLmsSystem.Domain.Entities.Common;
using EbuBridgeLmsSystem.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace EbuBridgeLmsSystem.Persistance.SeedDatas.Extensions
{
    public static class RoleExtension
    {
        public static async Task AddAllRolesToUser(RoleManager<IdentityRole> roleManager,AppUser appUser,UserManager<AppUser> userManager)
        {
            var roles = await CheckRolesExistence(roleManager);
            var userRoles = await userManager.GetRolesAsync(appUser);
            var missingRoles=roles.Except(userRoles).ToArray();
            if (missingRoles.Any())
            {
                var roleResult = await userManager.AddToRolesAsync(appUser, missingRoles);
                if (!roleResult.Succeeded)
                {
                    Console.WriteLine($"Failed to assign roles to {appUser.UserName}: {string.Join(", ", roleResult.Errors.Select(e => e.Description))}");
                }
            }
            else
            {
                var roleResult = await userManager.AddToRolesAsync(appUser, roles);
                if (!roleResult.Succeeded)
                {
                    Console.WriteLine($"Failed to assign roles to {appUser.UserName}: {string.Join(", ", roleResult.Errors.Select(e => e.Description))}");
                }
            }
           
            
        }
        public static async Task<string[]> CheckRolesExistence(RoleManager<IdentityRole> roleManager)
        {
            var roles = Enum.GetNames(typeof(RolesEnum));
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    var result = await roleManager.CreateAsync(new IdentityRole { Name = role, NormalizedName = role.ToUpper() });
                    if (!result.Succeeded)
                    {
                        Console.WriteLine($"Failed to create role {role}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                    }
                }
            }
            return roles;
        }
    }
}
