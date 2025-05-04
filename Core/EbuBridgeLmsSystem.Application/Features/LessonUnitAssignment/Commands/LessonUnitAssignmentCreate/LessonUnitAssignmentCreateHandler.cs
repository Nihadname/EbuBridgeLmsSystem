using EbuBridgeLmsSystem.Application.Interfaces;
using EbuBridgeLmsSystem.Domain.Entities;
using EbuBridgeLmsSystem.Domain.Entities.Common;
using EbuBridgeLmsSystem.Domain.Repositories;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EbuBridgeLmsSystem.Application.Features.LessonUnitAssignment.Commands.LessonUnitAssignmentCreate
{
    public class LessonUnitAssignmentCreateHandler : IRequestHandler<LessonUnitAssignmentCreateCommand, Result<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppUserResolver _appUserResolver;
        public LessonUnitAssignmentCreateHandler(IUnitOfWork unitOfWork, IAppUserResolver appUserResolver)
        {
            _unitOfWork = unitOfWork;
            _appUserResolver = appUserResolver;
        }

        public async Task<Result<Unit>> Handle(LessonUnitAssignmentCreateCommand request, CancellationToken cancellationToken)
        {
            var isLessonUnitExist=await _unitOfWork.LessonUnitRepository.isExists(s=>s.Id==request.LessonUnitId&&!s.IsDeleted);
            if(!isLessonUnitExist)
                return Result<Unit>.Failure(Error.NotFound,null,ErrorType.NotFoundError);
            var isStudentExist=await _unitOfWork.StudentRepository.isExists(s => s.Id == request.StudentId && !s.IsDeleted);
            if(!isStudentExist)
                return Result<Unit>.Failure(Error.NotFound, null, ErrorType.NotFoundError);
            var curentUserInTheSystem = await _appUserResolver.GetCurrentUserAsync(s => !s.IsDeleted,includes: new Func<IQueryable<AppUser>, IQueryable<AppUser>>[]{
               q => q.Include(p => p.Teacher).ThenInclude(p=>p.lessonStudentTeachers) });
            if(curentUserInTheSystem == null||curentUserInTheSystem.Teacher == null)
            {
                return Result<Unit>.Failure(Error.Unauthorized,null,ErrorType.UnauthorizedError);
            }
            var isStudentInTeacherList=curentUserInTheSystem.Teacher.lessonStudentTeachers.Select(s=>s.Student).Any(s=>s.Id == request.StudentId);
            if (!isStudentInTeacherList)
                return Result<Unit>.Failure(Error.Custom("Student", "student is not one of the in the list of this teacher"),null, ErrorType.BusinessLogicError);
            
            throw new NotImplementedException();        }
    }
}
