using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using EbuBridgeLmsSystem.Application.Exceptions;
using EbuBridgeLmsSystem.Application.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using EbuBridgeLmsSystem.Application.Interfaces;
using EbuBridgeLmsSystem.Infrastructure.Concretes;
using Resend;

namespace EbuBridgeLmsSystem.Infrastructure
{
    public static class InfrastructureServiceRegisteration
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
          services.Configure<CloudinarySettings>(
  configuration.GetSection("CloudinarySettings"));
           services.AddSingleton(provider =>
            {
                var settings = provider.GetRequiredService<IOptions<CloudinarySettings>>().Value;

                Console.WriteLine($"Initializing Cloudinary with: {settings.CloudName}, {settings.ApiKey}, {settings.ApiSecret}");

                var account = new CloudinaryDotNet.Account(settings.CloudName, settings.ApiKey, settings.ApiSecret);
                var cloudinary = new Cloudinary(account);
                try
                {
                    var result = cloudinary.ListResources(new ListResourcesParams { MaxResults = 1 });
                    if (result.Error != null)
                    {
                        throw new CustomException(400, $"Cloudinary Account Error: {result.Error.Message}");
                    }
                }
                catch (Exception ex)
                {
                    throw new CustomException(400, ex.Message);
                }

                return cloudinary;
            });
            services.AddScoped<IPhotoOrVideoService, PhotoOrVideoService>();
            services.AddOptions();
            services.AddHttpClient<ResendClient>();
            services.Configure<ResendClientOptions>(configuration.GetSection("Resend"));

            services.AddTransient<IResend, ResendClient>();
            services.AddScoped<IEmailService, EmailService>();
        }
    }
}
