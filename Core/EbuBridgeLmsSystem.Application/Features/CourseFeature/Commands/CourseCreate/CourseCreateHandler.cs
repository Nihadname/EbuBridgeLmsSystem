using EbuBridgeLmsSystem.Application.Dtos.Course;
using EbuBridgeLmsSystem.Application.Exceptions;
using EbuBridgeLmsSystem.Application.Interfaces;
using EbuBridgeLmsSystem.Domain.Entities;
using EbuBridgeLmsSystem.Domain.Entities.Common;
using EbuBridgeLmsSystem.Domain.Repositories;
using Hangfire;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Features.CourseFeature.Commands.CourseCreate
{
    public class CourseCreateHandler : IRequestHandler<CourseCreateCommand, Result<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPhotoOrVideoService _photoOrVideoService;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly ILogger<CourseCreateHandler> _logger;
        public CourseCreateHandler(IUnitOfWork unitOfWork, IPhotoOrVideoService photoOrVideoService, ILogger<CourseCreateHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _photoOrVideoService = photoOrVideoService;
            _logger = logger;
        }

        public async Task<Result<Unit>> Handle(CourseCreateCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                var isDuplicateNameCourse = await _unitOfWork.CourseRepository.GetEntity(s => s.Name.Equals(request.Name, StringComparison.OrdinalIgnoreCase), AsnoTracking: true, isIgnoredDeleteBehaviour: true);
                if (isDuplicateNameCourse is not null)
                {
                    if (isDuplicateNameCourse.IsDeleted)
                    {
                        isDuplicateNameCourse.IsDeleted = false;
                        await _unitOfWork.SaveChangesAsync(cancellationToken);
                        return Result<Unit>.Success(Unit.Value);
                    }
                    return Result<Unit>.Failure(Error.DuplicateConflict, null, ErrorType.BusinessLogicError);
                }
                if (!Enum.TryParse<DifficultyLevel>(request.difficultyLevel.ToString(), out _))
                {
                    return Result<Unit>.Failure(Error.ValidationFailed, null, ErrorType.ValidationError);
                }
                var isExistedLanguage = await _unitOfWork.LanguageRepository.isExists(s => s.Id == request.LanguageId);
                if (!isExistedLanguage)
                {
                    return Result<Unit>.Failure(Error.NotFound, null, ErrorType.NotFoundError);
                }
                var newCourse = new Course()
                {
                    Name = request.Name,
                    Description = request.Description,
                    difficultyLevel = request.difficultyLevel,
                    LanguageId = request.LanguageId,
                    Duration = request.Duration,
                    Price = request.Price,
                    ImageUrl = null,
                    StartDate = request.StartDate ?? null,
                    EndDate = request.EndDate ?? null,
                };
                await _unitOfWork.CourseRepository.Create(newCourse);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync(cancellationToken);
                _backgroundJobClient.Enqueue(() => UploadImageToCloud(newCourse.Id, request.formFile));
                return Result<Unit>.Success(Unit.Value);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                _logger.LogError(ex, "Error occurred during user address create");
                return Result<Unit>.Failure(Error.InternalServerError, null, ErrorType.SystemError);
            }
        }

        [AutomaticRetry(Attempts = 3)]
        public async Task UploadImageToCloud(Guid id, IFormFile imageFile)
        {
            try
            {
                var imageUrl = await _photoOrVideoService.UploadMediaAsync(imageFile, false);
                var existedCourse=await _unitOfWork.CourseRepository.GetEntity(s=>s.Id == id,AsnoTracking:true);
                if (existedCourse is null)
                    throw new CustomException(404, "course  is null");
                existedCourse.ImageUrl = imageUrl;
                await _unitOfWork.CourseRepository.Update(existedCourse);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error uploading course image");
            }
        }
    }
}
