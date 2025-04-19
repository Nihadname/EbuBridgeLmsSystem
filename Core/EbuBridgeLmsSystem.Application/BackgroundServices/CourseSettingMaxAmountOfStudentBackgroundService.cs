using EbuBridgeLmsSystem.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EbuBridgeLmsSystem.Application.BackgroundServices
{
    public sealed class CourseSettingMaxAmountOfStudentBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<CourseImageUploadBackgroundService> _logger;
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                var allCourses = await unitOfWork.CourseRepository.GetAll(s=>s.IsDeleted);
                foreach (var course in allCourses)
                {
                    
                }
            }
                throw new NotImplementedException();
        }
    }
}
