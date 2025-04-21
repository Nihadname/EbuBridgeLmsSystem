using EbuBridgeLmsSystem.Application.Helpers.Extensions;
using EbuBridgeLmsSystem.Application.Interfaces;
using EbuBridgeLmsSystem.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EbuBridgeLmsSystem.Application.BackgroundServices
{
    public sealed class CourseImageUploadBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<CourseImageUploadBackgroundService> _logger;
        public CourseImageUploadBackgroundService(IServiceProvider serviceProvider, ILogger<CourseImageUploadBackgroundService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var unitOfWork=scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                var imageService=scope.ServiceProvider.GetRequiredService<IPhotoOrVideoService>();
                var outBoxQuery = await unitOfWork.CourseImageOutBoxRepository.GetQuery(s => s.OutboxProccess == Domain.Enums.OutboxProccess.Pending&& !s.IsDeleted);
                var pendingOutBoxes=outBoxQuery.OrderBy(s=>s.CreatedTime).ToList();
               
                foreach (var pendingOutBox in pendingOutBoxes)
                {
                    if (stoppingToken.IsCancellationRequested)
                        break;
                    try
                    {
                        var existedCourseWithId = await unitOfWork.CourseRepository.GetEntity(s => s.Id == pendingOutBox.CourseId&&!s.IsDeleted);
                        if(existedCourseWithId != null)
                        {
                            var fileName = ImageExtension.GetImageFileNameFromCourseId(existedCourseWithId.Id);
                            if (fileName != null)
                            {
                               var result= imageService.UploadMediaAsyncWithUrl(fileName);
                                if (result is not null)
                                {
                                    existedCourseWithId.ImageUrl=result;
                                    await unitOfWork.CourseRepository.Update(existedCourseWithId);
                                    await unitOfWork.SaveChangesAsync();
                                    pendingOutBox.OutboxProccess = Domain.Enums.OutboxProccess.Completed;
                                    await unitOfWork.CourseImageOutBoxRepository.Update(pendingOutBox);
                                    await unitOfWork.SaveChangesAsync(stoppingToken);
                                   fileName.DeleteFile();
                                }
                                else
                                {
                                    pendingOutBox.OutboxProccess=Domain.Enums.OutboxProccess.Failed;
                                    await unitOfWork.CourseImageOutBoxRepository.Update(pendingOutBox);
                                    await unitOfWork.SaveChangesAsync(stoppingToken);   
                                    _logger.LogError($"Failed to upload image for course {existedCourseWithId.Id}");
                                }
                            }
                        }
                         

                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"Error processing outbox for course: {ex.Message}", ex);
                    }
                }
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
            }
          
        }
    }

