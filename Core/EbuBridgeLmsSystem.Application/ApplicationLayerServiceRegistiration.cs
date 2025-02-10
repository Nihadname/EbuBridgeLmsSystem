using AutoMapper;
using EbuBridgeLmsSystem.Application.Profiles;
using EbuBridgeLmsSystem.Application.Validators.AuthValidators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

        }
    }
}
