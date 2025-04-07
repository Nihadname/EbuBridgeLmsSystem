using EbuBridgeLmsSystem.Application.Features.LessonMaterialFeature.Commands.LessonMaterialCreate;
using EbuBridgeLmsSystem.Application.Helpers.Extensions;
using EbuBridgeLmsSystem.Application.Interfaces;
using EbuBridgeLmsSystem.Domain.Entities;
using EbuBridgeLmsSystem.Domain.Entities.Common;
using EbuBridgeLmsSystem.Domain.Repositories;
using FluentValidation;
using Hangfire;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Features.LessonVideoFeature.Commands.LessonVideoFeatureCreate
{
    public sealed class LessonVideoCreateHandler : IRequestHandler<LessonVideoCreateCommand, Result<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IPhotoOrVideoService _photoOrVideoService;
        private readonly IValidator<LessonVideoCreateCommand> _validator;

        public LessonVideoCreateHandler(IValidator<LessonVideoCreateCommand> validator, IPhotoOrVideoService photoOrVideoService, IBackgroundJobClient backgroundJobClient, IUnitOfWork unitOfWork)
        {
            _validator = validator;
            _photoOrVideoService = photoOrVideoService;
            _backgroundJobClient = backgroundJobClient;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(LessonVideoCreateCommand request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid)
            {
                return Result<Unit>.Failure(null, validationResult.Errors.Select(s => s.ErrorMessage).ToList(), ErrorType.ValidationError);
            }
            var isExistedLesson = await _unitOfWork.LessonRepository.isExists(s => s.Id == request.LessonId && !s.IsDeleted);
            if (!isExistedLesson)
                return Result<Unit>.Failure(Error.NotFound, null, ErrorType.NotFoundError);
            var isExistedLessonMaterialInTheSameLesson = await _unitOfWork.LessonMaterialRepository
                 .isExists(s => s.Title.ToLower() == request.Title.ToLower() && s.LessonId == request.LessonId && !s.IsDeleted);
            if (isExistedLessonMaterialInTheSameLesson)
                return Result<Unit>.Failure(Error.Custom("LessonMaterial", "LessonMaterial with this title already exists in this lesson"), null, ErrorType.BusinessLogicError);
            try
            {
                var newLessonVideo = new LessonVideo()
                {
                    LessonId = request.LessonId,
                    Url = null,
                    Title = request.Title,
                };
                await _unitOfWork.LessonVideoRepository.Create(newLessonVideo);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();
                var fileBytes = await request.File.GetFileBytesAsync();
                var backgroundJobId = _backgroundJobClient.Enqueue(() => UploadLessonMaterialAsset(fileBytes, request.File.Name, request.File.ContentType, newLessonMaterial.Id));
                return Result<Unit>.Success(Unit.Value);
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
 
        }
    }
}
