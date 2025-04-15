using LearningManagementSystem.Core.Entities.Common;
using MediatR;

namespace EbuBridgeLmsSystem.Application.Features.LessonUnitFeature.Commands.LessonUnitCreate
{
    public sealed class LessonUnitCreateHandler : IRequestHandler<LessonUnitCreateCommand, Result<Unit>>
    {
        public Task<Result<Unit>> Handle(LessonUnitCreateCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
