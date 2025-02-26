
using EbuBridgeLmsSystem.Domain.Entities.Common;
using LearningManagementSystem.Core.Entities.Common;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace EbuBridgeLmsSystem.Api
{
    public static class ServiceRegisteration
    {
        public static void Register(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddFluentValidationRulesToSwagger();
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "SmartEccommerceApi",
                    Version = "v1"
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
            });
            services.AddSwaggerGen(c =>
            {
                c.MapType<TimeSpan>(() => new Microsoft.OpenApi.Models.OpenApiSchema
                {
                    Type = "string",
                    Example = new Microsoft.OpenApi.Any.OpenApiString("00:00:00") // Default example for TimeSpan
                });

                // Other Swagger configuration (e.g., c.SwaggerDoc)
            });

            services.AddControllersWithViews()
           .ConfigureApiBehaviorOptions(opt =>
           {
               opt.InvalidModelStateResponseFactory = context =>
               {
                   var errorsValidation = context.ModelState
                      .Where(e => e.Value?.Errors.Count > 0)
                      .ToDictionary(
                          x => x.Key,
                          x => x.Value.Errors.First().ErrorMessage
                      );
                   List<string> errors = new List<string>();
                   foreach (KeyValuePair<string, string> keyValues in errorsValidation)
                   {
                       errors.Add(keyValues.Key + " " + keyValues.Value);
                   }

                   var response = Result<string>.Failure(Error.ValidationFailed, errors, ErrorType.ValidationError);

                   return new BadRequestObjectResult(response);
               };
           });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });
        }
    }
}
