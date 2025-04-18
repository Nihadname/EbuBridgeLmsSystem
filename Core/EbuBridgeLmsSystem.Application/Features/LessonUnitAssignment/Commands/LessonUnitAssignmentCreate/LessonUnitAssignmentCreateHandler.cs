using EbuBridgeLmsSystem.Domain.Repositories;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;

namespace EbuBridgeLmsSystem.Application.Features.LessonUnitAssignment.Commands.LessonUnitAssignmentCreate
{
    public class LessonUnitAssignmentCreateHandler : IRequestHandler<LessonUnitAssignmentCreateCommand, Result<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public LessonUnitAssignmentCreateHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Result<Unit>> Handle(LessonUnitAssignmentCreateCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();        }
    }
}
