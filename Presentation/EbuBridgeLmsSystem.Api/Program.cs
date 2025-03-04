using EbuBridgeLmsSystem.Api;
using EbuBridgeLmsSystem.Api.Middlewares;
using EbuBridgeLmsSystem.Api.MinimalEndPoints.Admin;
using EbuBridgeLmsSystem.Application;
using EbuBridgeLmsSystem.Domain.Entities;
using EbuBridgeLmsSystem.Infrastructure;
using EbuBridgeLmsSystem.Persistance;
using EbuBridgeLmsSystem.Persistance.Data;
using EbuBridgeLmsSystem.Persistance.SeedDatas;
using Hangfire;
using MicroElements.OpenApi.FluentValidation;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var config = builder.Configuration;
var baseAdminUrl = config["ApiSettings:BaseAdminUrl"];
var clientSideUrl=config["ApiSettings:ClientSideUrl"];
builder.Services.Register(config);
builder.Services.AddPersistenceServices(config);
builder.Services.AddInfrastructureServices(config);
builder.Services.RegisterApplicationLayerServices(config);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpClient();
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapAuthEndpoints(baseAdminUrl);
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseHangfireDashboard();
app.UseRouting();
app.UseCors(x=>x.AllowAnyHeader()
.AllowCredentials()
.AllowAnyMethod()
.SetIsOriginAllowed(t=>true)
.SetPreflightMaxAge(TimeSpan.FromMinutes(10)));
app.UseMiddleware<CustomExceptionMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});
app.MapControllers();
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    var context = services.GetRequiredService<ApplicationDbContext>();
    var userManager = services.GetRequiredService<UserManager<AppUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

    await context.Database.MigrateAsync();
    await UserSeed.SeedAdminUserAsync(userManager, roleManager);
    await UserSeed.SeedUserWhoHaveAllRoles(services);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "error during migration");
};
app.Run();
