using AutoMapper;
using EbuBridgeLmsSystem.Application.BackgroundServices;
using EbuBridgeLmsSystem.Application.Behaviours;
using EbuBridgeLmsSystem.Application.Features.AppUserFeature.Commands.CreateAppUserAsStudent;
using EbuBridgeLmsSystem.Application.Profiles;
using EbuBridgeLmsSystem.Application.Validators.AuthValidators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stripe;

namespace EbuBridgeLmsSystem.Application
{
    public static class ApplicationLayerServiceRegistiration
    {
        public static void RegisterApplicationLayerServices(this IServiceCollection serviceDescriptors, IConfiguration configuration)
        {
            serviceDescriptors.AddFluentValidationAutoValidation()
            .AddFluentValidationClientsideAdapters()
            .AddValidatorsFromAssemblyContaining<RegisterValidator>();
            serviceDescriptors.AddSingleton<IMapper>(provider =>
            {
                var scopeFactory = provider.GetRequiredService<IServiceScopeFactory>();

                using var scope = scopeFactory.CreateScope();
                var httpContextAccessor = scope.ServiceProvider.GetRequiredService<IHttpContextAccessor>();
                //var photoOrVideoService = scope.ServiceProvider.GetRequiredService<IPhotoOrVideoService>();

                var mapperConfig = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new MapperProfile(httpContextAccessor));
                });

                return mapperConfig.CreateMapper();
            });
            serviceDescriptors.AddHangfire(x => x.UseSqlServerStorage(configuration.GetConnectionString("AppConnectionString")));
            serviceDescriptors.AddHangfireServer();
            var stripeSettings= configuration.GetSection("Stripe");
            StripeConfiguration.ApiKey = stripeSettings["SecretKey"];
            serviceDescriptors.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(CreateAppUserAsStudentHandler).Assembly);
            });
            serviceDescriptors.AddHostedService<UserPermanentDeleteBackgroundService>();
            serviceDescriptors.AddHostedService<CourseImageUploadBackgroundService>();
            serviceDescriptors.AddResponseCompression(opt =>
            {
                opt.EnableForHttps = true;
                opt.Providers.Add<BrotliCompressionProvider>();
                opt.Providers.Add<GzipCompressionProvider>();
            });
            serviceDescriptors.AddTransient(typeof(IPipelineBehavior<,>), typeof(FluentValidationBehavior<,>));

        }
    }
}
