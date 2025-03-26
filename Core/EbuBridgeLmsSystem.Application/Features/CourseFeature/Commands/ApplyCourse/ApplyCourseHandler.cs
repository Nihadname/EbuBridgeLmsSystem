using EbuBridgeLmsSystem.Domain.Entities;
using EbuBridgeLmsSystem.Domain.Entities.Common;
using EbuBridgeLmsSystem.Domain.Repositories;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Features.CourseFeature.Commands.ApplyCourse
{
    public sealed class ApplyCourseHandler : IRequestHandler<ApplyCourseCommand, Result<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;

        public ApplyCourseHandler(IUnitOfWork unitOfWork, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<Result<Unit>> Handle(ApplyCourseCommand request, CancellationToken cancellationToken)
        {
          var existedStudent=await _unitOfWork.StudentRepository.GetEntity(s=>s.Id==request.StudentId,true, includes: new Func<IQueryable<Student>, IQueryable<Student>>[] {
                 query => query
            .Include(p => p.courseStudents) });
            if(existedStudent is null)
                return Result<Unit>.Failure(Error.Custom("student","student doesnt exist"),null,ErrorType.NotFoundError);
            var isExistedCourse=await _unitOfWork.CourseRepository.isExists(s=>s.Id == request.CourseId);
            if(!isExistedCourse)
                return Result<Unit>.Failure(Error.Custom("course", "course doesnt exist"), null, ErrorType.NotFoundError);
            var allCoursesStudentIsIn = existedStudent.courseStudents;
            var isAlreadyApplied = allCoursesStudentIsIn.Any(course => course.CourseId == request.CourseId);
            if (isAlreadyApplied)
                return Result<Unit>.Failure(Error.Custom("course", "you already applied for this course"), null, ErrorType.BusinessLogicError);

            var newCourseStudent = new CourseStudent()
            {
                EnrolledDate = DateTime.UtcNow,
                CourseId = request.CourseId,
                StudentId = request.StudentId
            };
            await _unitOfWork.CourseStudentRepository.Create(newCourseStudent);
            await _unitOfWork.SaveChangesAsync();
            return Result<Unit>.Success(Unit.Value);

        }
    }
}
