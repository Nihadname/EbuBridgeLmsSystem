using EbuBridgeLmsSystem.Application;
using EbuBridgeLmsSystem.Infrastructure;
using EbuBridgeLmsSystem.Persistance;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var config = builder.Configuration;
var app = builder.Build();

builder.Services.AddPersistenceServices(config);
builder.Services.AddInfrastructureServices(config);
builder.Services.RegisterApplicationLayerServices(config);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
