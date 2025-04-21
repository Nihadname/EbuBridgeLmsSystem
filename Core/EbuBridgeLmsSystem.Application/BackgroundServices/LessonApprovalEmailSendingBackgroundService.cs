using EbuBridgeLmsSystem.Application.Interfaces;
using EbuBridgeLmsSystem.Domain.Entities;
using EbuBridgeLmsSystem.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EbuBridgeLmsSystem.Application.BackgroundServices
{
    public class LessonApprovalEmailSendingBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<LessonApprovalEmailSendingBackgroundService> _logger;
        public LessonApprovalEmailSendingBackgroundService(IServiceProvider serviceProvider, ILogger<LessonApprovalEmailSendingBackgroundService> logger)
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
                var outBoxQuery = await unitOfWork.LessonStudentStudentApprovalOutBoxRepository.GetQuery(s => s.OutboxProccess == Domain.Enums.OutboxProccess.Pending);
                var pendingOutBoxes = outBoxQuery.OrderBy(s => s.CreatedTime).ToList();
                foreach (var outbox in pendingOutBoxes)
                {
                    if (stoppingToken.IsCancellationRequested)
                        break;
                    try
                    {
                        var existedLessonStudentWithOutBoxId=await unitOfWork.LessonStudentRepository.GetEntity(s=>s.Id==outbox.Id&&!s.IsDeleted&&s.isApproved, includes: new Func<IQueryable<LessonStudent>, IQueryable<LessonStudent>>[] {
                 query => query
            .Include(p => p.Student).ThenInclude(s=>s.AppUser).Include(s=>s.Lesson) });
                        if(existedLessonStudentWithOutBoxId is not null)
                        {
                            var body = $"<h1>Welcome!</h1><p>Thank you for joining us. We're excited to have you!, this is your confirmation towards attending this lesson ${existedLessonStudentWithOutBoxId.Lesson.Title} </p>";
                            await emailService.SendEmailAsync(existedLessonStudentWithOutBoxId.Student.AppUser.Email, "Course approval", body, true);
                            outbox.OutboxProccess = Domain.Enums.OutboxProccess.Completed;
                            await unitOfWork.LessonStudentStudentApprovalOutBoxRepository.Update(outbox);
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
