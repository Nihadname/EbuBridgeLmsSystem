﻿using EbuBridgeLmsSystem.Application.Helpers.Extensions;
using EbuBridgeLmsSystem.Application.Interfaces;
using EbuBridgeLmsSystem.Domain.Entities;
using EbuBridgeLmsSystem.Domain.Entities.Common;
using EbuBridgeLmsSystem.Domain.Entities.LmsSystem;
using EbuBridgeLmsSystem.Domain.Repositories;
using FluentValidation;
using Hangfire;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EbuBridgeLmsSystem.Application.Features.LessonVideoFeature.Commands.LessonVideoFeatureCreate
{
    public sealed class LessonVideoCreateHandler : IRequestHandler<LessonVideoCreateCommand, Result<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IPhotoOrVideoService _photoOrVideoService;
 

        public LessonVideoCreateHandler(IPhotoOrVideoService photoOrVideoService, IBackgroundJobClient backgroundJobClient, IUnitOfWork unitOfWork)
        {
            _photoOrVideoService = photoOrVideoService;
            _backgroundJobClient = backgroundJobClient;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(LessonVideoCreateCommand request, CancellationToken cancellationToken)
        {
            var isExistedLessonUnit = await _unitOfWork.LessonRepository.isExists(s => s.Id == request.LessonUnitId && !s.IsDeleted);
            if (!isExistedLessonUnit)
                return Result<Unit>.Failure(Error.NotFound, null, ErrorType.NotFoundError);
            var isExistedLessonUnitVideoInTheSameLesson = await _unitOfWork.LessonUnitVideoRepository
                 .isExists(s => s.Title.ToLower() == request.Title.ToLower() && s.LessonUnitId == request.LessonUnitId && !s.IsDeleted);
            if (isExistedLessonUnitVideoInTheSameLesson)
                return Result<Unit>.Failure(Error.Custom("lessonVideo", "lessonVideo with this title already exists in this lesson"), null, ErrorType.BusinessLogicError);
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var newLessonVideo = new LessonUnitVideo()
                {
                    LessonUnitId = request.LessonUnitId,
                    Url = null,
                    Title = request.Title,
                };
                await _unitOfWork.LessonUnitVideoRepository.Create(newLessonVideo);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();
                var tempFilePath = await ImageExtension.SaveToTempLocation(request.File);
                var backgroundJobId = _backgroundJobClient.Enqueue(() => UploadLessonMaterialAsset(tempFilePath, request.File.Name, request.File.ContentType, newLessonVideo.Id));
                return Result<Unit>.Success(Unit.Value, SuccessReturnType.Created);
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
 
        }
        [AutomaticRetry(Attempts = 3)]
        private async Task<Result<string>> UploadLessonMaterialAsset(string tempFilePath, string fileName, string contentType, Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                    return Result<string>.Failure(Error.ValidationFailed, null, ErrorType.ValidationError);
                string resultUrl;
                using (var fileStream = new FileStream(tempFilePath, FileMode.Open))
                {
                    var formFile = new FormFile(fileStream, 0, fileStream.Length, "file", fileName)
                    {
                        Headers = new HeaderDictionary(),
                        ContentType = contentType
                    };

                    resultUrl = await _photoOrVideoService.UploadMediaAsync(formFile, true);
                }
                if (File.Exists(tempFilePath))
                    File.Delete(tempFilePath);
                await _unitOfWork.BeginTransactionAsync();
                try
                {
                    var existedLessonMaterial = await _unitOfWork.LessonUnitVideoRepository.GetEntity(s => s.Id == id && !s.IsDeleted);
                    if (existedLessonMaterial == null)
                    {
                        return Result<string>.Failure(Error.NotFound, null, ErrorType.NotFoundError);
                    }
                    existedLessonMaterial.Url = resultUrl;
                    await _unitOfWork.SaveChangesAsync();
                    await _unitOfWork.CommitTransactionAsync();
                }
                catch
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    throw;
                }
                return Result<string>.Success(resultUrl, null);
            }
            catch (Exception ex)
            {
                return Result<string>.Failure(Error.Custom(null, $"Error uploading file: {ex.Message}"), null, ErrorType.SystemError);

            }


        }
    }
}
