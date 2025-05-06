using EbuBridgeLmsSystem.Domain.Entities;
using EbuBridgeLmsSystem.Domain.Entities.Common;
using EbuBridgeLmsSystem.Domain.Entities.LmsSystem;
using EbuBridgeLmsSystem.Domain.Repositories;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EbuBridgeLmsSystem.Application.Features.CourseFeature.Commands.ApproveStudentCourseRequest
{
    public sealed class ApproveStudentCourseRequestHandler : IRequestHandler<ApproveStudentCourseRequestCommand, Result<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ApproveStudentCourseRequestHandler> _logger;
        public ApproveStudentCourseRequestHandler(IUnitOfWork unitOfWork, ILogger<ApproveStudentCourseRequestHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Result<Unit>> Handle(ApproveStudentCourseRequestCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                var existedCourseStudentRequest = await _unitOfWork.CourseStudentRepository.GetEntity(s => s.Id == request.Id && !s.IsDeleted);
                if (existedCourseStudentRequest == null)
                    return Result<Unit>.Failure(Error.NotFound, null, ErrorType.NotFoundError);
                existedCourseStudentRequest.isApproved = true;
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                var courseApprovalOutBox = new CourseStudentApprovalOutBox()
                {
                    CourseStudentId = existedCourseStudentRequest.Id,
                    OutboxProccess = Domain.Enums.OutboxProccess.Pending,
                    CreatedTime = DateTime.UtcNow,
                };
                await _unitOfWork.CourseStudentApprovalOutBoxRepository.Create(courseApprovalOutBox);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync(cancellationToken);
                return Result<Unit>.Success(Unit.Value, SuccessReturnType.NoContent);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                _logger.LogError(ex, "Error occurred during user address create");
                return Result<Unit>.Failure(Error.InternalServerError, null, ErrorType.SystemError);
            }

        }
    }
}
