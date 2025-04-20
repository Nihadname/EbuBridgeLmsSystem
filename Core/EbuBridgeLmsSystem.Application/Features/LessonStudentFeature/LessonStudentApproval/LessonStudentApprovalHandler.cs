using EbuBridgeLmsSystem.Domain.Entities;
using EbuBridgeLmsSystem.Domain.Entities.Common;
using EbuBridgeLmsSystem.Domain.Repositories;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EbuBridgeLmsSystem.Application.Features.LessonStudentFeature.LessonStudentApproval
{
    public sealed class LessonStudentApprovalHandler : IRequestHandler<LessonStudentApprovalCommand, Result<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<LessonStudentApprovalHandler> _logger;
        public LessonStudentApprovalHandler(IUnitOfWork unitOfWork, ILogger<LessonStudentApprovalHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<Result<Unit>> Handle(LessonStudentApprovalCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
             var existedLessonStudent=await _unitOfWork.LessonStudentRepository.GetEntity(s=>s.Id==request.LessonStudentId&&!s.IsDeleted);
                if (existedLessonStudent == null)
                    return Result<Unit>.Failure(Error.NotFound, null, ErrorType.NotFoundError);
                existedLessonStudent.isApproved = true;
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                LessonStudentStudentApprovalOutBox lessonStudentStudentApprovalOutBox = new()
                {
                    LessonStudentId = existedLessonStudent.Id,
                    OutboxProccess=Domain.Enums.OutboxProccess.Pending,
                    CreatedTime = DateTime.UtcNow,
                };
                await _unitOfWork.LessonStudentStudentApprovalOutBoxRepository.Create(lessonStudentStudentApprovalOutBox);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync(cancellationToken);
                return Result<Unit>.Success(Unit.Value, SuccessReturnType.NoContent);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                _logger.LogError(ex, "Error occurred during approving lesssonStudent");
                return Result<Unit>.Failure(Error.InternalServerError, null, ErrorType.SystemError);
            }
        }
    }
}
