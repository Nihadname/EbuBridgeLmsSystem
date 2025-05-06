using EbuBridgeLmsSystem.Domain.Entities.Common;
using EbuBridgeLmsSystem.Domain.Entities.LmsSystem;
using EbuBridgeLmsSystem.Domain.Repositories;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;

namespace EbuBridgeLmsSystem.Application.Features.LessonFeature.Commands.LessonCreate
{
    public sealed class LessonCreateHandler : IRequestHandler<LessonCreateCommand, Result<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public LessonCreateHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(LessonCreateCommand request, CancellationToken cancellationToken)
        {
            var isCourseExist=await _unitOfWork.CourseRepository.isExists(s=>s.Id==request.CourseId&&!s.IsDeleted);
            if (!isCourseExist)
                return Result<Unit>.Failure(Error.NotFound, null, ErrorType.NotFoundError);
            var isLessonWithRequestNameExist=await _unitOfWork.LessonRepository.GetEntity(s=>s.Title.ToLower()==request.Title.ToLower());
            if (isLessonWithRequestNameExist is not null)
            {
                if(isLessonWithRequestNameExist.IsDeleted is true)
                {
                    isLessonWithRequestNameExist.IsDeleted = false;
                    await _unitOfWork.SaveChangesAsync(cancellationToken);
                }
                return Result<Unit>.Failure(Error.DuplicateConflict, null, ErrorType.BusinessLogicError);
            }
            if (!Enum.TryParse<LessonStatus>(request.Status.ToString(), out _))
            {
                return Result<Unit>.Failure(Error.ValidationFailed, null, ErrorType.ValidationError);
            }
            if (!Enum.TryParse<LessonType>(request.LessonType.ToString(), out _))
            {
                return Result<Unit>.Failure(Error.ValidationFailed, null, ErrorType.ValidationError);
            }
            var newLesson=new Lesson() { 
                Title=request.Title,
                Status=request.Status,
                Description =request.Description,   
                CourseId=request.CourseId,
                GradingPolicy=request.GradingPolicy,
                LessonType=request.LessonType,
            };
            await _unitOfWork.LessonRepository.Create(newLesson);
            await _unitOfWork.SaveChangesAsync();
            return Result<Unit>.Success(Unit.Value, SuccessReturnType.Created);
        }
    }
}
