using EbuBridgeLmsSystem.Application.Dtos.Auth;
using EbuBridgeLmsSystem.Application.Interfaces;
using EbuBridgeLmsSystem.Domain.Entities;
using EbuBridgeLmsSystem.Domain.Entities.Common;
using EbuBridgeLmsSystem.Domain.Repositories;
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
        public LessonStudentCreateHandler(IUnitOfWork unitOfWork, IAppUserResolver userResolver, ILogger<LessonStudentCreateHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _userResolver = userResolver;
            _logger = logger;
        }

        public async Task<Result<Unit>> Handle(LessonStudentCreateCommand request, CancellationToken cancellationToken)
        {
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
                    return Result<Unit>.Failure(Error.Unauthorized, null, ErrorType.UnauthorizedError);
                }
                var existedStudent = await _unitOfWork.StudentRepository.GetEntity(s => s.Id == request.StudentId && s.AppUserId == currentUserInTheSystem.Id && !s.IsDeleted, includes: new Func<IQueryable<Student>, IQueryable<Student>>[]{
               query => query
            .Include(p => p.courseStudents).ThenInclude(p => p.Student).Include(p => p.courseStudents).ThenInclude(s=>s.Course)
            });
                if (existedStudent == null)
                {
                    await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                    return Result<Unit>.Failure(Error.Custom("Student", "student either doesnt exist either is not the same user who trying to apply"), null, ErrorType.BusinessLogicError);
                }
                var existedLesson = await _unitOfWork.LessonRepository.GetEntity(s => s.Id == request.LessonId && !s.IsDeleted);
                if (existedLesson == null)
                {
                    await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                    return Result<Unit>.Failure(Error.NotFound, null, ErrorType.NotFoundError);
                }
                var isTheLessonInTheCourseStudentIsIn = existedStudent.courseStudents.Select(courseStudent => courseStudent.Course)
                    .Any(course => course.lessons.Any(courseLesson => courseLesson.Id == request.LessonId && !courseLesson.IsDeleted) && !course.IsDeleted);
                if (!isTheLessonInTheCourseStudentIsIn)
                {
                    await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                    return Result<Unit>.Failure(Error.Custom("Student", "student tried to apply for a lesson that doesnt exist in  his/her applied courses"), null, ErrorType.BusinessLogicError);
                }
                var isUncompletedCourseLessonsExist = existedStudent.lessonStudents.Any(s => !s.isFinished);
                if (isUncompletedCourseLessonsExist)
                {
                    await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                    return Result<Unit>.Failure(Error.Custom("Course lesson", $"there are uncompeleted courses so you cant apply to this course {existedLesson.Title}  "), null, ErrorType.BusinessLogicError);
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
    }
}
