using EbuBridgeLmsSystem.Application.Interfaces;
using EbuBridgeLmsSystem.Domain.Entities;
using EbuBridgeLmsSystem.Domain.Entities.Common;
using EbuBridgeLmsSystem.Domain.Repositories;
using FluentValidation;
using Hangfire;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;

namespace EbuBridgeLmsSystem.Application.Features.LessonMaterialFeature.Commands.LessonMaterialCreate
{
    public sealed class LessonMaterialCreateHandler : IRequestHandler<LessonMaterialCreateCommand,Result<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IPhotoOrVideoService _photoOrVideoService;
        private readonly IValidator<LessonMaterialCreateCommand> _validator;
        public LessonMaterialCreateHandler(IBackgroundJobClient backgroundJobClient, IUnitOfWork unitOfWork, IPhotoOrVideoService photoOrVideoService, IValidator<LessonMaterialCreateCommand> validator)
        {
            _backgroundJobClient = backgroundJobClient;
            _unitOfWork = unitOfWork;
            _photoOrVideoService = photoOrVideoService;
            _validator = validator;
        }

        public async Task<Result<Unit>> Handle(LessonMaterialCreateCommand request, CancellationToken cancellationToken)
        {
            var validationResult=_validator.Validate(request);
            if (!validationResult.IsValid)
            {
             return   Result<Unit>.Failure(null, validationResult.Errors.Select(s=>s.ErrorMessage).ToList(), ErrorType.ValidationError);
            }
            var isExistedLesson=await _unitOfWork.LessonRepository.isExists(s=>s.Id==request.LessonId&&!s.IsDeleted);
            if (!isExistedLesson)
                return Result<Unit>.Failure(Error.NotFound, null, ErrorType.NotFoundError);
            var isExistedLessonMaterialInTheSameLesson = await _unitOfWork.LessonMaterialRepository
                 .isExists(s => s.Title.ToLower() == request.Title.ToLower() && s.LessonId == request.LessonId&&!s.IsDeleted);
            if (isExistedLessonMaterialInTheSameLesson)
                return Result<Unit>.Failure(Error.Custom("LessonMaterial", "LessonMaterial with this title already exists in this lesson"), null, ErrorType.BusinessLogicError);
            var newLessonMaterial = new LessonMaterial()
            {
                LessonId = request.LessonId,
                Url = null,
                Title = request.Title,
            };
            await _unitOfWork.LessonMaterialRepository.Create(newLessonMaterial);
            await _unitOfWork.SaveChangesAsync();
            var backgroundJobId = _backgroundJobClient.Enqueue(() => UploadLessonMaterialAsset(request,newLessonMaterial.Id));
            return Result<Unit>.Success(Unit.Value);
        }
        [AutomaticRetry(Attempts = 3)]
        private async Task<Result<string>> UploadLessonMaterialAsset(LessonMaterialCreateCommand request,Guid id)
        {
            try
            {
                if(id== Guid.Empty)
                    return Result<string>.Failure(Error.ValidationFailed, null, ErrorType.ValidationError);
                var contentType = request.File.ContentType.ToLower();
                var extension = Path.GetExtension(request.File.FileName).ToLower();
                var imageExtension = extension == ".jpg" || extension == ".jpeg" || extension == ".png" || extension == ".gif" || extension == ".webp" || extension == ".svg";
                string resultUrl;
                if (contentType.StartsWith("image/") && imageExtension)
                {
                    resultUrl = await _photoOrVideoService.UploadMediaAsync(request.File);
                }
                else
                {
                    resultUrl = await _photoOrVideoService.UploadMediaAsync(request.File, false, true);
                }


                var existedLessonMaterial=await _unitOfWork.LessonMaterialRepository.GetEntity(s=>s.Id == id&&!s.IsDeleted);
                if (existedLessonMaterial == null)
                {
                    return Result<string>.Failure(Error.NotFound, null, ErrorType.NotFoundError);
                }
                existedLessonMaterial.Url = resultUrl;
                await _unitOfWork.SaveChangesAsync();
                return Result<string>.Success(resultUrl);
            }
            catch (Exception ex)
            {
                return Result<string>.Failure(Error.Custom(null, $"Error uploading file: {ex.Message}"),null, ErrorType.SystemError);

            }


        }
    }
}
