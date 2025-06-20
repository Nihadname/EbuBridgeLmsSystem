﻿using EbuBridgeLmsSystem.Application.Helpers.Extensions;
using EbuBridgeLmsSystem.Domain.Entities;
using EbuBridgeLmsSystem.Domain.Entities.Common;
using EbuBridgeLmsSystem.Domain.Entities.LmsSystem;
using EbuBridgeLmsSystem.Domain.Enums;
using EbuBridgeLmsSystem.Domain.Repositories;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EbuBridgeLmsSystem.Application.Features.CourseFeature.Commands.UpdateCourse
{
    public class UpdateCourseHandler : IRequestHandler<UpdateCourseCommand, Result<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCourseHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            var existingCourseResult=await GetExistingCourse(request.CourseId);
            if (!existingCourseResult.IsSuccess)
                return Result<Unit>.FailureResult<Course, Unit>(existingCourseResult);
            bool hasChanges =false;
            var checkNameChangeResult = await UpdateCourseName(request.Name, existingCourseResult.Data);
            if (!checkNameChangeResult.IsSuccess)
                return Result<Unit>.FailureResult<bool, Unit>(checkNameChangeResult);
            var checkIfDifficultyLevelChangedResult = UpdateCourseDifficultyLevel(request, existingCourseResult.Data);
            if (!checkIfDifficultyLevelChangedResult.IsSuccess)
               return Result<Unit>.FailureResult<bool,Unit>(checkIfDifficultyLevelChangedResult);
            var checkIfCourseLanguageChangedResult= await UpdateCourseLanguage(request, existingCourseResult.Data);
               if(!checkIfCourseLanguageChangedResult.IsSuccess)
                return Result<Unit>.FailureResult<bool, Unit>(checkIfCourseLanguageChangedResult);
            var IsDurationChanged=UpdateCourseDuration(request, existingCourseResult.Data);
            var isPriceChanged = UpdateCoursePrice(request, existingCourseResult.Data);
            var isCourseDetailsResult = UpdateCourseDetails(request.Description, request.Requirements, existingCourseResult.Data);
            await UpdateCourseImage(request, existingCourseResult.Data);
            hasChanges |= checkNameChangeResult.Data;
            hasChanges |= isCourseDetailsResult;
            hasChanges |= checkIfCourseLanguageChangedResult.Data;
            hasChanges |= checkIfDifficultyLevelChangedResult.Data;
            hasChanges |= isPriceChanged;
            hasChanges |= IsDurationChanged;
            if (hasChanges)
            {
                await _unitOfWork.CourseRepository.Update(existingCourseResult.Data);
                await _unitOfWork.SaveChangesAsync();
            }
            return Result<Unit>.Success(Unit.Value, null);
        }
        private async Task<Result<Course>> GetExistingCourse(Guid courseId)
        {
            if(courseId == Guid.Empty)
                throw new ArgumentNullException(nameof(courseId));
            var existedCourse = await _unitOfWork.CourseRepository.GetEntity(s => s.Id == courseId & !s.IsDeleted,AsnoTracking:true);
            if (existedCourse == null)
                return Result<Course>.Failure(Error.NotFound, null, ErrorType.NotFoundError);
            return Result<Course>.Success(existedCourse, null);   
        }
        private async Task<Result<bool>> UpdateCourseName(string name, Course existingCourse)
        {
            if(string.IsNullOrWhiteSpace(name)) return Result<bool>.Success(false,null);
            var existedCourseWithThisName = await _unitOfWork.CourseRepository.GetEntity(s => EF.Functions.Like(s.Name, $"%{name}%") && s.Id != existingCourse.Id, AsnoTracking: true);
            if(existedCourseWithThisName is not null)
            {
                if(existedCourseWithThisName.IsDeleted)
                    return Result<bool>.Failure(Error.Custom("Course Name", "This Course was previously deleted. Consider renaming the existing record instead of restoring."), null, ErrorType.BusinessLogicError);
                return Result<bool>.Failure(Error.Custom("Course Name", "It already exists"), null, ErrorType.BusinessLogicError);
            }
            existingCourse.Name = name;
            return Result<bool>.Success(true, null); 
        }
        private bool UpdateCourseDetails(string description,string requirements, Course existingCourse)
        {
            bool hasChanges=false;
            if (string.IsNullOrWhiteSpace(description))
            {
                existingCourse.Description =description;
                hasChanges = true;
            }
            if (string.IsNullOrWhiteSpace(requirements))
            {
                existingCourse.Requirements = requirements;
                hasChanges = true;
            }
            return hasChanges;    
        }
        private async Task UpdateCourseImage(UpdateCourseCommand request, Course existingCourse)
        {
            if (request.formFile != null)
            {
                var temporaryImage = request.formFile.Save(existingCourse.Id);
                var newCourseImageOutBox = new CourseImageOutBox()
                {
                    CourseId = existingCourse.Id,
                    OutboxProccess = OutboxProccess.Pending,
                    CreatedTime = DateTime.UtcNow,
                    TemporaryImagePath = temporaryImage,
                };
                await _unitOfWork.CourseImageOutBoxRepository.Create(newCourseImageOutBox);
                await _unitOfWork.SaveChangesAsync();
            }
        }
        private  Result<bool> UpdateCourseDifficultyLevel(UpdateCourseCommand request, Course existingCourse)
        {
            if(!request.difficultyLevel.HasValue) return Result<bool>.Success(false,null);
            if (Enum.TryParse<DifficultyLevel>(request.difficultyLevel.ToString(), out var difficulty))
            {
                existingCourse.DifficultyLevel = difficulty;
                return Result<bool>.Success(true, null);
            }
            return Result<bool>.Failure(Error.BadRequest,null,ErrorType.ValidationError);
        }
        private async Task<Result<bool>> UpdateCourseLanguage(UpdateCourseCommand request, Course existingCourse)
        {
            if (request?.LanguageIds?.Count==0) return Result<bool>.Success(false,null);
            existingCourse.CourseLanguages.Clear();
            List<CourseLanguage> newLanguages = new();
            foreach (var languageId in request.LanguageIds)
            {
                var existedLanguage=await _unitOfWork.LanguageRepository.GetEntity(s => s.Id == languageId);
                if (existedLanguage is null)
                {
                    return Result<bool>.Failure(Error.NotFound, null, ErrorType.NotFoundError);

                }

                newLanguages.Add(new CourseLanguage()
                {
                  LanguageId = languageId,
                  CourseId = request.CourseId,
                });

            }
             existingCourse.CourseLanguages = newLanguages;
            return Result<bool>.Success(true, null);
        }
        private bool UpdateCoursePrice(UpdateCourseCommand request, Course existingCourse)
        {
            if (!request.Price.HasValue) return false;

            existingCourse.Price = request.Price.Value;
            return true;
        }
        private bool UpdateCourseDuration(UpdateCourseCommand request, Course existingCourse)
        {
            if (!request.DurationHours.HasValue) return false;

            existingCourse.DurationInHours = request.DurationHours.Value;
            return true;
        }
       
    }
}
