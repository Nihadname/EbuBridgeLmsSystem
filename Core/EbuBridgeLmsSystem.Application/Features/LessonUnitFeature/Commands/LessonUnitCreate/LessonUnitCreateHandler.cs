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
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                return Result<Unit>.Failure(Error.ValidationFailed, validationResult.Errors.Select(s => s.ErrorMessage).ToList(), ErrorType.ValidationError);
            }
            var isExistLesson=await _unitOfWork.LessonRepository.isExists(s=>s.Id==request.LessonId);
            if (isExistLesson)
                return Result<Unit>.Failure(Error.NotFound, null, ErrorType.NotFoundError);


            throw new NotImplementedException();
        }
    }
}
