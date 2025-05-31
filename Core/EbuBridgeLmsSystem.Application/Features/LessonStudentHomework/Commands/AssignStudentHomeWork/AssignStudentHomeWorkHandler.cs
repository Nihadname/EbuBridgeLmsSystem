using EbuBridgeLmsSystem.Application.Helpers.Extensions.Auth;
using EbuBridgeLmsSystem.Application.Interfaces;
using EbuBridgeLmsSystem.Domain.Entities.Common;
using EbuBridgeLmsSystem.Domain.Repositories;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EbuBridgeLmsSystem.Application.Features.LessonStudentHomework.Commands.AssignStudentHomeWork;

public sealed class AssignStudentHomeWorkHandler:IRequestHandler<AssignStudentHomeWorkCommand, Result<Unit>>
{
    private readonly string TeacherRole="Teacher";
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppUserResolver _userResolver;
    private readonly UserManager<AppUser>  _userManager;

    public AssignStudentHomeWorkHandler(IUnitOfWork unitOfWork, IAppUserResolver userResolver, UserManager<AppUser> userManager)
    {
        _unitOfWork = unitOfWork;
        _userResolver = userResolver;
        _userManager = userManager;
    }
    
    public async Task<Result<Unit>> Handle(AssignStudentHomeWorkCommand request, CancellationToken cancellationToken)
    { var currentUser = await _userResolver.GetCurrentUserAsync(s => !s.IsDeleted,includes: new Func<IQueryable<AppUser>, IQueryable<AppUser>>[]{
            q => q.Include(p => p.Teacher).ThenInclude(p=>p.lessonStudentTeachers) });
        if(currentUser == null)
            return Result<Unit>.Failure(Error.Unauthorized, null,ErrorType.UnauthorizedError);
        var isUserTeacher  =await AuthExtension.CheckExistenceOfRoleInAppUser(_userManager, currentUser,TeacherRole ,null);
        if (!isUserTeacher)
        {
            return Result<Unit>.Failure(Error.Unauthorized, null,ErrorType.UnauthorizedError);
        }
        var existedStudent=await _unitOfWork.StudentRepository.GetEntity(s=>s.Id == request.StudentId);
        var existedLessonUnit = await _unitOfWork.LessonUnitRepository.GetEntity(s => s.Id == request.LessonUnitId);
        if (existedStudent == null || existedLessonUnit == null)
        {
            return Result<Unit>.Failure(Error.NotFound, null, ErrorType.NotFoundError);
        }
        
        throw new NotImplementedException();
    }
}