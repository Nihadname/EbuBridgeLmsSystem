using EbuBridgeLmsSystem.Domain.Entities;
using EbuBridgeLmsSystem.Domain.Entities.Common;
using EbuBridgeLmsSystem.Domain.Repositories;
using FluentValidation;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;

namespace EbuBridgeLmsSystem.Application.Features.LessonUnitFeature.Commands.LessonUnitCreate
{
    public sealed class LessonUnitCreateHandler : IRequestHandler<LessonUnitCreateCommand, Result<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<LessonUnitCreateCommand> _validator;
        public LessonUnitCreateHandler(IUnitOfWork unitOfWork, IValidator<LessonUnitCreateCommand> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<Result<Unit>> Handle(LessonUnitCreateCommand request, CancellationToken cancellationToken)
        {
            var isExistLesson=await _unitOfWork.LessonRepository.isExists(s=>s.Id==request.LessonId);
            if (isExistLesson)
                return Result<Unit>.Failure(Error.NotFound, null, ErrorType.NotFoundError);
            var newLessonUnit = new LessonUnit()
            {
                LessonId = request.LessonId,
                LessonSetTime = DateTime.UtcNow,
                Name = request.Name,
            };
            await _unitOfWork.LessonUnitRepository.Create(newLessonUnit);
            await _unitOfWork.SaveChangesAsync();
            return Result<Unit>.Success(Unit.Value, SuccessReturnType.Created);
        }
    }
}
