using EbuBridgeLmsSystem.Application.Dtos.Auth;
using EbuBridgeLmsSystem.Application.Interfaces;
using EbuBridgeLmsSystem.Domain.Entities;
using EbuBridgeLmsSystem.Domain.Entities.Common;
using EbuBridgeLmsSystem.Domain.Repositories;
using FluentValidation;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EbuBridgeLmsSystem.Application.Features.LessonStudentFeature.Commands.LessonStudentCreate
{
    public sealed class LessonStudentCreateHandler : IRequestHandler<LessonStudentCreateCommand, Result<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppUserResolver _userResolver;
        private readonly ILogger<LessonStudentCreateHandler> _logger;
        private readonly  IValidator<LessonStudentCreateCommand> _validator;
        public LessonStudentCreateHandler(IUnitOfWork unitOfWork, IAppUserResolver userResolver, ILogger<LessonStudentCreateHandler> logger, IValidator<LessonStudentCreateCommand> validator)
        {
            _unitOfWork = unitOfWork;
            _userResolver = userResolver;
            _logger = logger;
            _validator = validator;
        }

        public async Task<Result<Unit>> Handle(LessonStudentCreateCommand request, CancellationToken cancellationToken)
        {
            var validationResult= _validator.Validate(request);
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                var currentUserInTheSystem = await _userResolver.GetCurrentUserAsync(includes: new Func<IQueryable<AppUser>, IQueryable<AppUser>>[]{
               query => query
            .Include(p => p.Student)
            });
                if (currentUserInTheSystem == null)
                {
                    await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                    return UnauthorizedError();
                }
                var existedStudent = await _unitOfWork.StudentRepository.GetEntity(s => s.Id == request.StudentId && s.AppUserId == currentUserInTheSystem.Id && !s.IsDeleted, includes: new Func<IQueryable<Student>, IQueryable<Student>>[]{
               query => query
            .Include(p => p.courseStudents).ThenInclude(p => p.Student).Include(p => p.courseStudents).ThenInclude(s=>s.Course)
            });
                if (existedStudent == null)
                {
                    await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                    return StudentNotFoundError();
                }
                var existedLesson = await _unitOfWork.LessonRepository.GetEntity(s => s.Id == request.LessonId && !s.IsDeleted);
                if (existedLesson == null)
                {
                    await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                    return LessonNotFoundError();
                }
                var isTheLessonInTheCourseStudentIsIn = existedStudent.courseStudents.Select(courseStudent => courseStudent.Course)
                    .Any(course => course.lessons.Any(courseLesson => courseLesson.Id == request.LessonId && !courseLesson.IsDeleted) && !course.IsDeleted);
                if (!isTheLessonInTheCourseStudentIsIn)
                {
                    await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                    return LessonNotInCourseError(existedLesson.Title);
                }
                var isUncompletedCourseLessonsExist = existedStudent.lessonStudents.Any(s => !s.isFinished);
                if (isUncompletedCourseLessonsExist)
                {
                    await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                    return UncompletedLessonsError(existedLesson.Title);
                }
                var lessonStudent = new LessonStudent()
                {
                    LessonId = request.LessonId,
                    StudentId = request.StudentId,
                    isFinished = false,
                };
                await _unitOfWork.LessonStudentRepository.Create(lessonStudent);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync(cancellationToken);
                return Result<Unit>.Success(Unit.Value);

            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                _logger.LogError(ex, "Error occurred during lessonStudent create");
                return Result<Unit>.Failure(Error.InternalServerError, null, ErrorType.SystemError);

            }
        }
        private Result<Unit> UnauthorizedError() =>
        Result<Unit>.Failure(Error.Unauthorized, null, ErrorType.UnauthorizedError);

        private Result<Unit> StudentNotFoundError() =>
            Result<Unit>.Failure(Error.Custom("Student", "Student doesn't exist or not authorized"), null, ErrorType.BusinessLogicError);

        private Result<Unit> LessonNotFoundError() =>
            Result<Unit>.Failure(Error.NotFound, null, ErrorType.NotFoundError);

        private Result<Unit> LessonNotInCourseError(string lessonTitle) =>
            Result<Unit>.Failure(Error.Custom("Lesson", $"Lesson '{lessonTitle}' not in student's courses"), null, ErrorType.BusinessLogicError);

        private Result<Unit> UncompletedLessonsError(string lessonTitle) =>
            Result<Unit>.Failure(Error.Custom("Lesson", $"Uncompleted lessons exist, cannot apply to '{lessonTitle}'"), null, ErrorType.BusinessLogicError);

        private Result<Unit> InternalServerError() =>
            Result<Unit>.Failure(Error.InternalServerError, null, ErrorType.SystemError);

    }
}
