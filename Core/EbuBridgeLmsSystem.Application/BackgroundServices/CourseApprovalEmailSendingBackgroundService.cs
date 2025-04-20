using EbuBridgeLmsSystem.Application.Interfaces;
using EbuBridgeLmsSystem.Domain.Entities;
using EbuBridgeLmsSystem.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EbuBridgeLmsSystem.Application.BackgroundServices
{
    internal class CourseApprovalEmailSendingBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<CourseApprovalEmailSendingBackgroundService> _logger;
        public CourseApprovalEmailSendingBackgroundService(IServiceProvider serviceProvider, ILogger<CourseApprovalEmailSendingBackgroundService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();
                var Backgro
                var outBoxQuery = await unitOfWork.LessonStudentStudentApprovalOutBoxRepository.GetQuery(s => s.OutboxProccess == Domain.Enums.OutboxProccess.Pending);
                var pendingOutBoxes = outBoxQuery.OrderBy(s => s.CreatedTime).ToList();
                foreach (var pendingOutBox in pendingOutBoxes)
                {
                    if (stoppingToken.IsCancellationRequested)
                        break;
                    try
                    {
                        var existedCourseStudentWithOutBoxId = await unitOfWork.CourseStudentRepository.GetEntity(s => s.Id == pendingOutBox.Id && !s.IsDeleted && s.isApproved, includes: new Func<IQueryable<CourseStudent>, IQueryable<CourseStudent>>[] {
                 query => query
            .Include(p => p.Student).ThenInclude(s=>s.AppUser) });
                        if (existedCourseStudentWithOutBoxId is not null)
                        {
                            var body = $"<h1>Welcome!</h1><p>Thank you for joining us. We're excited to have you!, this is your confirmation towards attending this course </p>";
                            await emailService.SendEmailAsync(existedCourseStudentWithOutBoxId.Student.AppUser.Email, "Course approval", body, true);
                            pendingOutBox.OutboxProccess = Domain.Enums.OutboxProccess.Completed;
                            await unitOfWork.LessonStudentStudentApprovalOutBoxRepository.Update(pendingOutBox);
                            await unitOfWork.SaveChangesAsync(stoppingToken);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, $"Error processing outbox for course: {ex.Message}");

                    }
                }

            }
            await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);

        }
    }
}