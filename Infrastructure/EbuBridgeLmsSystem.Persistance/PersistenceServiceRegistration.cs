using EbuBridgeLmsSystem.Application.Interfaces;
using EbuBridgeLmsSystem.Application.Settings;
using EbuBridgeLmsSystem.Domain.Entities.Common;
using EbuBridgeLmsSystem.Domain.Repositories;
using EbuBridgeLmsSystem.Persistance.AuthHandler.HttpAuth;

using EbuBridgeLmsSystem.Persistance.Data;
using EbuBridgeLmsSystem.Persistance.Data.Implementations;
using EbuBridgeLmsSystem.Persistance.Processors;
using LearningManagementSystem.Core.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EbuBridgeLmsSystem.Persistance
{
    public static class PersistenceServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("AppConnectionString"),
                b => b.MigrationsAssembly("EbuBridgeLmsSystem.Persistance"))
            );
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("Redis");
                options.InstanceName = "MyApp_";
            });
            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireDigit = true;
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.User.RequireUniqueEmail = true;
            }).AddDefaultTokenProviders().AddEntityFrameworkStores<ApplicationDbContext>();
            services.Configure<JwtSettings>(configuration.GetSection("Jwt"));
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
       .AddJwtBearer(options =>
       {
           options.TokenValidationParameters = new TokenValidationParameters
           {
               ValidateIssuer = true,
               ValidateAudience = true,
               ValidateLifetime = true,
               ValidateIssuerSigningKey = true,
               ValidIssuer = configuration["Jwt:Issuer"],
               ValidAudience = configuration["Jwt:Audience"],
               IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"])),
               ClockSkew = TimeSpan.Zero
           };
       });
            
            services.AddScoped<IAppUserResolver,AppUserResolver>();
            //services.AddScoped<IAuthService,AuthService>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<ITeacherRepository, TeacherRepository>();
            services.AddScoped<IParentRepository, ParentRepository>();
            services.AddScoped<IRequstToRegisterRepository, RequstToRegisterRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<INoteRepository, NoteRepository>();
            services.AddScoped<IReportRepository, ReportRepository>();
            services.AddScoped<IReportOptionRepository, ReportOptionRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IFeeRepository, FeeRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IAuditLogRepository, AuditLogRepository>();
            services.AddScoped<ILanguageRepository, LanguageRepository>();
            services.AddScoped<ICourseImageOutBoxRepository, CourseImageOutBoxRepository>();
            services.AddScoped<ICourseStudentRepository, CourseStudentRepository>();
            services.AddScoped<ILessonUnitMaterialRepository, LessonUnitMaterialRepository>();
            services.AddScoped<ILessonRepository, LessonRepository>();
            services.AddScoped<ILessonUnitVideoRepository, LessonUnitVideoRepository>();
            services.AddScoped<ILessonStudentRepository, LessonStudentRepository>();
            services.AddScoped<ILessonUnitRepository, LessonUnitRepository>();
            services.AddScoped<ILessonUnitAttendanceRepository, LessonUnitAttendanceRepository>();
            services.AddScoped<ICourseStudentApprovalOutBoxRepository, CourseStudentApprovalOutBoxRepository>();
            services.AddScoped<ILessonUnitAssignmentRepository, LessonUnitAssignmentRepository>();
            services.AddScoped<ILessonStudentStudentApprovalOutBoxRepository, LessonStudentStudentApprovalOutBoxRepository>();
            services.AddScoped<ICourseTeacherRepository, CourseTeacherRepository>();
            services.AddScoped<ICourseTeacherLessonRepository, CourseTeacherLessonRepository>();
            services.AddScoped<IChatMessageRepository, ChatMessageRepository>();
            services.AddScoped<ISaasStudentRepository, SaasStudentRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

        }
    }
}
