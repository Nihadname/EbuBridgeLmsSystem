using EbuBridgeLmsSystem.Application.Dtos.Course;
using EbuBridgeLmsSystem.Application.Interfaces;
using EbuBridgeLmsSystem.Domain.Entities;
using EbuBridgeLmsSystem.Domain.Entities.Common;
using EbuBridgeLmsSystem.Domain.Repositories;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
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
        public CourseCreateHandler(IUnitOfWork unitOfWork, IPhotoOrVideoService photoOrVideoService)
        {
            _unitOfWork = unitOfWork;
            _photoOrVideoService = photoOrVideoService;
        }

        public async Task<Result<Unit>> Handle(CourseCreateCommand request, CancellationToken cancellationToken)
        {
            var isDuplicateNameCourse=await _unitOfWork.CourseRepository.GetEntity(s=>s.Name.Equals(request.Name,StringComparison.OrdinalIgnoreCase), AsnoTracking: true, isIgnoredDeleteBehaviour: true);
            if (isDuplicateNameCourse is not null)
            {
                if (isDuplicateNameCourse.IsDeleted)
                {
                    isDuplicateNameCourse.IsDeleted = false;
                    await _unitOfWork.SaveChangesAsync(cancellationToken);
                    return Result<Unit>.Success(Unit.Value);
                }
              return Result<Unit>.Failure(Error.DuplicateConflict,null,ErrorType.BusinessLogicError); 
            }
            if (!Enum.IsDefined(typeof(DifficultyLevel), request.difficultyLevel)||!Enum.IsDefined(typeof(Language),request.Language))
            {
                return Result<Unit>.Failure(Error.ValidationFailed,null,ErrorType.ValidationError);
            }
            var newCourse=new Course()
            {
                Name = request.Name,
                Description = request.Description,
                difficultyLevel = request.difficultyLevel,
                Language=request.Language,

            }
            throw new NotImplementedException();
        }
    }
}
