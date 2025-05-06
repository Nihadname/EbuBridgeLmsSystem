using EbuBridgeLmsSystem.Domain.Entities.Common;
using EbuBridgeLmsSystem.Domain.Entities.LmsSystem;
using EbuBridgeLmsSystem.Domain.Repositories;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace EbuBridgeLmsSystem.Application.Features.LessonStudentFeature.Commands.LessonStudentApproval
{
    public sealed class LessonStudentTeacherApprovalHandler : IRequestHandler<LessonStudentTeacherApprovalCommand, Result<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<LessonStudentTeacherApprovalHandler> _logger;
        public LessonStudentTeacherApprovalHandler(IUnitOfWork unitOfWork, ILogger<LessonStudentTeacherApprovalHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<Result<Unit>> Handle(LessonStudentTeacherApprovalCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                var existedLessonStudent = await _unitOfWork.LessonStudentRepository.GetEntity(s => s.Id == request.LessonStudentId && !s.IsDeleted);
                if (existedLessonStudent == null)
                    return Result<Unit>.Failure(Error.NotFound, null, ErrorType.NotFoundError);
                var existedTeacher=await _unitOfWork.TeacherRepository.GetEntity(s => s.Id == request.TeacherId&&!s.IsDeleted);
                if(existedTeacher is null)
                    return Result<Unit>.Failure(Error.NotFound, null, ErrorType.NotFoundError);
                var isTeacherInTheCourseOfStudent = existedTeacher.CourseTeachers
     .Any(teacher => teacher.Id == existedTeacher.Id &&
         teacher.CourseTeacherLessons
             .Any(lesson => lesson.LessonId == existedLessonStudent.LessonId));
                if(!isTeacherInTheCourseOfStudent)
                    return Result<Unit>.Failure(Error.Custom("TeacherAddition","the teacher you are trying to assign to this is not in these course or lesson"), null, ErrorType.BusinessLogicError);

                existedLessonStudent.isApproved = true;
                existedLessonStudent.TeacherId = request.TeacherId;
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                LessonStudentStudentApprovalOutBox lessonStudentStudentApprovalOutBox = new()
                {
                    LessonStudentId = existedLessonStudent.Id,
                    OutboxProccess = Domain.Enums.OutboxProccess.Pending,
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
