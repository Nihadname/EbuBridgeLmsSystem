using EbuBridgeLmsSystem.Application.AppDefaults;
using EbuBridgeLmsSystem.Domain.Entities;
using EbuBridgeLmsSystem.Persistance.Data;
using EbuBridgeLmsSystem.Persistance.SeedDatas.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Data;

namespace EbuBridgeLmsSystem.Persistance.SeedDatas
{
    public class UserSeed
    {
        public static async Task SeedAdminUserAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var adminEmail = "admin@example.com";
            var adminPassword = "Admin@12345";

            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var adminUser = new AppUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    Image = AppDefaultValue.DefaultProfileImageUrl,
                    fullName = "Admin User",
                    CreatedTime = DateTime.UtcNow,
                    IsEmailVerificationCodeValid = true
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
                else
                {
                    throw new Exception("Failed to create admin user: " + string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }
        }
        public static async Task SeedUserWhoHaveAllRoles(IServiceProvider serviceDescriptors)
        {
            using var scope = serviceDescriptors.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            dbContext.Database.EnsureCreated();
            if (dbContext.Database.GetPendingMigrations().Any()) await dbContext.Database.MigrateAsync();
            await RoleExtension.CheckRolesExistence(roleManager);
            var users = new Dictionary<string, string>
        {
            { "TeymurDevv@gmail.com", "12345@Tt" },
            { "TeymurDevv2@gmail.com", "12345@Tt" },
            { "Nadir810@gmail.com", "12345@Nn" },
            { "Nadir811@gmail.com", "12345@Nn" },
             {"nihadsatexam@gmail.com","12345@Tt" }
        };
            foreach (var (email, password) in users)
            {
                var existingUser = await userManager.FindByEmailAsync(email);
                if (existingUser == null)
                {
                    var user = new AppUser
                    {
                        fullName = Guid.NewGuid().ToString("N")[..7],
                        UserName = email,
                        NormalizedUserName = email.ToUpper(),
                        Email = email,
                        Image = null,
                        NormalizedEmail = email.ToUpper(),
                        PhoneNumber = null,
                        IsEmailVerificationCodeValid = true
                    };

                    var result = await userManager.CreateAsync(user, password);
                    if (!result.Succeeded)
                    {
                        Console.WriteLine($"Failed to create user {email}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                        continue;
                    }

                    existingUser = await userManager.FindByEmailAsync(email);
                }
                if (existingUser != null)
                {
                    await RoleExtension.AddAllRolesToUser(roleManager, existingUser,userManager);
                }


            }
        }
    }
}