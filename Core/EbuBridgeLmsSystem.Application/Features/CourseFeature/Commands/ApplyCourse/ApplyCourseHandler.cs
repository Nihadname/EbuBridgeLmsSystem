﻿using EbuBridgeLmsSystem.Application.Exceptions;
using EbuBridgeLmsSystem.Application.Interfaces;
using EbuBridgeLmsSystem.Domain.Entities.Common;
using EbuBridgeLmsSystem.Domain.Entities.LmsSystem;
using EbuBridgeLmsSystem.Domain.Repositories;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EbuBridgeLmsSystem.Application.Features.CourseFeature.Commands.ApplyCourse
{
    public sealed class ApplyCourseHandler : IRequestHandler<ApplyCourseCommand, Result<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;
       
        private readonly ILogger<ApplyCourseHandler> _logger;
        private readonly IAppUserResolver _appUserResolver;

        public ApplyCourseHandler(IUnitOfWork unitOfWork, ILogger<ApplyCourseHandler> logger, IAppUserResolver appUserResolver)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _appUserResolver = appUserResolver;
        }

        public async Task<Result<Unit>> Handle(ApplyCourseCommand request, CancellationToken cancellationToken)
        {
                var currentUserInSystem = await _appUserResolver.GetCurrentUserAsync();
            if (currentUserInSystem == null)
                return Result<Unit>.Failure(Error.Unauthorized, null, ErrorType.UnauthorizedError);
                var existedStudent = await _unitOfWork.StudentRepository.GetEntity(s => s.Id == request.StudentId&&!s.IsDeleted&&s.AppUserId==currentUserInSystem.Id, true, includes: new Func<IQueryable<Student>, IQueryable<Student>>[] {
                 query => query
            .Include(p => p.courseStudents) });
                if (existedStudent is null)
                    return Result<Unit>.Failure(Error.Custom("student", "student doesnt exist or you arent the user who is atteched to this given student"), null, ErrorType.NotFoundError);
                var existedCourse = await _unitOfWork.CourseRepository.GetEntity(s => s.Id == request.CourseId);
                if (existedCourse is null)
                    return Result<Unit>.Failure(Error.Custom("course", "course doesnt exist"), null, ErrorType.NotFoundError);
                var allCoursesStudentIsIn = existedStudent.courseStudents;
                var isAlreadyApplied = allCoursesStudentIsIn.Any(course => course.CourseId == request.CourseId);
                if (isAlreadyApplied)
                    return Result<Unit>.Failure(Error.Custom("course", "you already applied for this course"), null, ErrorType.BusinessLogicError);
                if((await _unitOfWork.CourseStudentRepository.GetAll(s=>s.CourseId == request.CourseId)).Count >= existedCourse.MaxAmountOfPeople)
                {
                    throw new CustomException(400, "there is already enough students");
                }
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                var newCourseStudent = new CourseStudent()
                {
                    EnrolledDate = DateTime.UtcNow,
                    CourseId = request.CourseId,
                    StudentId = request.StudentId,
                   isApproved=false
                };
                existedStudent.IsEnrolledInAnyCourse = true;
                await _unitOfWork.CourseStudentRepository.Create(newCourseStudent);
                await _unitOfWork.StudentRepository.Update(existedStudent);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync(cancellationToken);
                return Result<Unit>.Success(Unit.Value, SuccessReturnType.NoContent);
            }catch(Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                _logger.LogError(ex, "Error occurred during user registration");
                return Result<Unit>.Failure(Error.InternalServerError, null, ErrorType.SystemError);
            }

        }
    }
}
