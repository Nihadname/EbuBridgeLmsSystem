using EbuBridgeLmsSystem.Application.Helpers.Extensions;
using EbuBridgeLmsSystem.Application.Interfaces;
using EbuBridgeLmsSystem.Domain.Entities;
using EbuBridgeLmsSystem.Domain.Entities.Common;
using EbuBridgeLmsSystem.Domain.Repositories;
using FluentValidation;
using Hangfire;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EbuBridgeLmsSystem.Application.Features.LessonMaterialFeature.Commands.LessonMaterialCreate
{
    public sealed class LessonMaterialCreateHandler : IRequestHandler<LessonMaterialCreateCommand,Result<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IPhotoOrVideoService _photoOrVideoService;
        public LessonMaterialCreateHandler(IBackgroundJobClient backgroundJobClient, IUnitOfWork unitOfWork, IPhotoOrVideoService photoOrVideoService, IValidator<LessonMaterialCreateCommand> validator)
        {
            _backgroundJobClient = backgroundJobClient;
            _unitOfWork = unitOfWork;
            _photoOrVideoService = photoOrVideoService;
        }

        public async Task<Result<Unit>> Handle(LessonMaterialCreateCommand request, CancellationToken cancellationToken)
        {
           
            var isExistedLessonUnit=await _unitOfWork.LessonUnitRepository.isExists(s=>s.Id==request.LessonUnitId&&!s.IsDeleted);
            if (!isExistedLessonUnit)
                return Result<Unit>.Failure(Error.NotFound, null, ErrorType.NotFoundError);
            var isExistedLessonUnitMaterialInTheSameLesson = await _unitOfWork.LessonUnitMaterialRepository
                 .isExists(s => s.Title.ToLower() == request.Title.ToLower() && s.LessonUnitId == request.LessonUnitId&&!s.IsDeleted);
            if (isExistedLessonUnitMaterialInTheSameLesson)
                return Result<Unit>.Failure(Error.Custom("LessonMaterial", "LessonMaterial with this title already exists in this lesson"), null, ErrorType.BusinessLogicError);
            try
            {
                var newLessonMaterial = new LessonUnitMaterial()
                {
                    LessonUnitId = request.LessonUnitId,
                    Url = null,
                    Title = request.Title,
                };
                await _unitOfWork.LessonUnitMaterialRepository.Create(newLessonMaterial);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();
                var fileBytes = await request.File.GetFileBytesAsync();
                var backgroundJobId = _backgroundJobClient.Enqueue(() => UploadLessonMaterialAsset(fileBytes, request.File.Name, request.File.ContentType, newLessonMaterial.Id));
                return Result<Unit>.Success(Unit.Value,SuccessReturnType.Created);
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
        [AutomaticRetry(Attempts = 3)]
        private async Task<Result<string>> UploadLessonMaterialAsset(byte[] fileBytes,string fileName, string contentType,Guid id)
        {
            try
            {
                if(id== Guid.Empty)
                    return Result<string>.Failure(Error.ValidationFailed, null, ErrorType.ValidationError);
                using var memoryStream = new MemoryStream(fileBytes);
                var tempFile = new FormFile(memoryStream, 0, fileBytes.Length, "file", fileName)
                {
                    Headers = new HeaderDictionary(),
                    ContentType = contentType
                };
                var extension = Path.GetExtension(fileName).ToLower();
                var imageExtension = extension == ".jpg" || extension == ".jpeg" || extension == ".png" || extension == ".gif" || extension == ".webp" || extension == ".svg";
                string resultUrl;
                if (contentType.StartsWith("image/") && imageExtension)
                {
                    resultUrl = await _photoOrVideoService.UploadMediaAsync(tempFile);
                }
                else
                {
                    resultUrl = await _photoOrVideoService.UploadMediaAsync(tempFile, false, true);
                }

                await _unitOfWork.BeginTransactionAsync();
                try
                {
                    var existedLessonMaterial = await _unitOfWork.LessonUnitMaterialRepository.GetEntity(s => s.Id == id && !s.IsDeleted);
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
                return Result<string>.Failure(Error.Custom(null, $"Error uploading file: {ex.Message}"),null, ErrorType.SystemError);

            }


        }
    }
}
