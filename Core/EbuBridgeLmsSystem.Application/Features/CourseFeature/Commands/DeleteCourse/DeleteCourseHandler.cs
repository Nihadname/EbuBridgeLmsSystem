using EbuBridgeLmsSystem.Domain.Entities.Common;
using EbuBridgeLmsSystem.Domain.Repositories;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;

namespace EbuBridgeLmsSystem.Application.Features.CourseFeature.Commands.DeleteCourse
{
    public class DeleteCourseHandler : IRequestHandler<DeleteCourseCommand, Result<Unit>>
    {
        private readonly  IUnitOfWork _unitOfWork;

        public DeleteCourseHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            var existedCourse = await _unitOfWork.CourseRepository.GetEntity(s=>s.Id==request.Id&&!s.IsDeleted,AsnoTracking:true);
            if(existedCourse is null)
                return Result<Unit>.Failure(Error.NotFound,null,ErrorType.NotFoundError);
            await _unitOfWork.CourseRepository.Delete(existedCourse);
            await _unitOfWork.SaveChangesAsync();
            return Result<Unit>.Success(Unit.Value, SuccessReturnType.NoContent);

        }
    }
}
