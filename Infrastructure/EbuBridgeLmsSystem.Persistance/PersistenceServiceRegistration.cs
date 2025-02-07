using EbuBridgeLmsSystem.Persistance.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

        }
    }
}
