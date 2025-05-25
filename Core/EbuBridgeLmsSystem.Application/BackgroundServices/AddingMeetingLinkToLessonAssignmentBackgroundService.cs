using EbuBridgeLmsSystem.Application.Interfaces;
using EbuBridgeLmsSystem.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.BackgroundServices
{
    public class AddingMeetingLinkToLessonAssignmentBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<AddingMeetingLinkToLessonAssignmentBackgroundService> _logger;
        private static readonly string meetingUrlDefaultLink = "https://meet.jit.si/";

        public AddingMeetingLinkToLessonAssignmentBackgroundService(IServiceProvider serviceProvider, ILogger<AddingMeetingLinkToLessonAssignmentBackgroundService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Meeting link background service started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _serviceProvider.CreateScope();
                    var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                    var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();

                    var lessonsThatNeedMeetingLink = await unitOfWork.LessonUnitAssignmentRepository
                        .GetAll(s => s.ScheduledStartTime <= DateTime.UtcNow && s.LessonMeeting == null);

                    if (lessonsThatNeedMeetingLink.Count == 0)
                    {
                        _logger.LogInformation("No lessons require meeting links at this time.");
                    }
                    else
                    {
                        foreach (var lesson in lessonsThatNeedMeetingLink)
                        {
                            string roomName = GenerateRoomName();
                            string meetingUrl = $"{meetingUrlDefaultLink}{roomName}";

                            lesson.LessonMeeting = new Domain.Entities.LmsSystem.LessonUnitAssignment.Meeting()
                            {
                                Link = meetingUrl,
                                IsVerified = false,
                            };

                            _logger.LogInformation($"Added meeting link for lesson assignment {lesson.Id}: {meetingUrl}");

                            try
                            {
                                emailService.SendEmail(lesson.Student.AppUser.Email, "meting is created", "you will be able to join untill teacher verifies", true);
                            }
                            catch (Exception ex)
                            {
                                _logger.LogError(ex, "Error occurred in meeting link background service.");
                            }
                        }

                        await unitOfWork.SaveChangesAsync(stoppingToken);
                        _logger.LogInformation("Meeting links saved to database.");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred in meeting link background service.");
                }

               
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }

            _logger.LogInformation("Meeting link background service stopped.");
        }

        private static string GenerateRoomName()
        {
            return "MyMeetingRoom_" + Guid.NewGuid().ToString("N").Substring(0, 8);
        }
    }
}
