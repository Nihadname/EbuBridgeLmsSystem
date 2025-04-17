using EbuBridgeLmsSystem.Application.Interfaces;
using EbuBridgeLmsSystem.Domain.Entities;
using EbuBridgeLmsSystem.Domain.Entities.Common;
using EbuBridgeLmsSystem.Domain.Enums;
using EbuBridgeLmsSystem.Domain.Repositories;
using FluentValidation;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EbuBridgeLmsSystem.Application.Features.LessonStudentFeature.Commands.LessonStudentCreate
{
    public sealed class LessonStudentCreateHandler : IRequestHandler<LessonStudentCreateCommand, Result<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppUserResolver _userResolver;
        private readonly ILogger<LessonStudentCreateHandler> _logger;
        private readonly UserManager<AppUser> _userManager;
        public LessonStudentCreateHandler(IUnitOfWork unitOfWork, IAppUserResolver userResolver, ILogger<LessonStudentCreateHandler> logger, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userResolver = userResolver;
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<Result<Unit>> Handle(LessonStudentCreateCommand request, CancellationToken cancellationToken)
        {
                var currentUserInTheSystem = await _userResolver.GetCurrentUserAsync(s=>s.Student.Id==request.StudentId,includes: new Func<IQueryable<AppUser>, IQueryable<AppUser>>[]{
               q => q.Include(p => p.Student).ThenInclude(s => s.courseStudents).ThenInclude(cs => cs.Course).ThenInclude(c => c.lessons),
        q => q.Include(p => p.Student).ThenInclude(s => s.lessonStudents)
            });
               
                if (currentUserInTheSystem == null)
                    return UnauthorizedError();
                var userRoles=await _userManager.GetRolesAsync(currentUserInTheSystem);
            if (!userRoles.Any(s => s == RolesEnum.Student.ToString()) || currentUserInTheSystem.Student is null)
            {
                return Result<Unit>.Failure(Error.Custom("User", "User is not student  or not in student role"), null, ErrorType.BusinessLogicError);
            }
            var existedStudent =  currentUserInTheSystem.Student;
                if (existedStudent == null)
                    return StudentNotFoundError();
                var existedLesson = await _unitOfWork.LessonRepository.GetEntity(s => s.Id == request.LessonId && !s.IsDeleted);
                if (existedLesson == null)
                    return LessonNotFoundError();
                var isTheLessonInTheCourseStudentIsIn = existedStudent.courseStudents.Select(courseStudent => courseStudent.Course)
                    .Any(course => course.lessons.Any(courseLesson => courseLesson.Id == request.LessonId && !courseLesson.IsDeleted) && !course.IsDeleted);
                if (!isTheLessonInTheCourseStudentIsIn)
                {
                    return LessonNotInCourseError(existedLesson.Title);
                }
                var isUncompletedCourseLessonsExist = existedStudent.lessonStudents.Any(s => !s.isFinished);
                if (isUncompletedCourseLessonsExist)
                {
                    return UncompletedLessonsError(existedLesson.Title);
                }
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                var lessonStudent = new LessonStudent()
                {
                    LessonId = request.LessonId,
                    StudentId = request.StudentId,
                    isFinished = false,
                };
                await _unitOfWork.LessonStudentRepository.Create(lessonStudent);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync(cancellationToken);
                return Result<Unit>.Success(Unit.Value, SuccessReturnType.Created);

            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                _logger.LogError(ex, "Error occurred during lessonStudent create");
               return InternalServerError();

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
