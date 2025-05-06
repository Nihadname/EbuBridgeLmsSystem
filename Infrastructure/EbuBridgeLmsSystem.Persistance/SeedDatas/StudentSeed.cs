using EbuBridgeLmsSystem.Application.AppDefaults;
using EbuBridgeLmsSystem.Domain.Entities;
using EbuBridgeLmsSystem.Domain.Entities.Common;
using EbuBridgeLmsSystem.Domain.Entities.LmsSystem;
using EbuBridgeLmsSystem.Domain.Enums;
using EbuBridgeLmsSystem.Persistance.Data;
using EbuBridgeLmsSystem.Persistance.SeedDatas.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EbuBridgeLmsSystem.Persistance.SeedDatas
{
    public class StudentSeed
    {
        public static async Task CreateStudentForAppUser(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            var studentEmail = "student@example.com";
            var studentPassword = "StudentUser@12345";
            dbContext.Database.EnsureCreated();
            if (dbContext.Database.GetPendingMigrations().Any()) await dbContext.Database.MigrateAsync();
            var userAssociatedToNewStudent = await userManager.FindByEmailAsync(studentEmail);
            if(userAssociatedToNewStudent == null)
            {
                var studentUser = new AppUser
                {
                    UserName = studentEmail,
                    Email = studentEmail,
                    EmailConfirmed = true,
                    Image = AppDefaultValue.DefaultProfileImageUrl,
                    fullName = "Student User",
                    CreatedTime = DateTime.UtcNow,
                    IsEmailVerificationCodeValid = true
                };
                var result=await userManager.CreateAsync(studentUser,studentPassword);
                var newStudent =new Student() {AppUserId=studentUser.Id };
                await dbContext.AddAsync(newStudent);
                await dbContext.SaveChangesAsync();
                await RoleExtension.CheckRolesExistence(roleManager);
                if (result.Succeeded)
                {
                    await RoleExtension.AddAllRolesToUser(roleManager, studentUser, userManager);
                }
                else
                {
                    throw new Exception("Failed to create admin user: " + string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }

        }
    }
}
