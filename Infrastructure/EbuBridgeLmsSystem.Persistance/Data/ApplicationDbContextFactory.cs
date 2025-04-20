using EbuBridgeLmsSystem.Persistance.Processors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Persistance.Data
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            Console.WriteLine("Factory used!");

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            Console.WriteLine("Factory used!2");

            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) 
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var connectionString = config.GetConnectionString("AppConnectionString");
            optionsBuilder.UseSqlServer(connectionString);
            var services = new ServiceCollection();
            services.AddScoped<IAuditLogProcessor, AuditLogProcessor>();

            var serviceProvider = services.BuildServiceProvider();
            Console.WriteLine("Factory used!3");

            return new ApplicationDbContext(optionsBuilder.Options,serviceProvider);
        }
    }
}
