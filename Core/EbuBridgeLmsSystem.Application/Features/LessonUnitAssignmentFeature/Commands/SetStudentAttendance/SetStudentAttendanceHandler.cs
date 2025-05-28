using EbuBridgeLmsSystem.Application.Helpers.Extensions.Auth;
using EbuBridgeLmsSystem.Application.Interfaces;
using EbuBridgeLmsSystem.Domain.Entities.Common;
using EbuBridgeLmsSystem.Domain.Entities.LmsSystem;
using EbuBridgeLmsSystem.Domain.Repositories;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EbuBridgeLmsSystem.Application.Features.LessonUnitAssignmentFeature.Commands.SetStudentAttendance;

public sealed class SetStudentAttendanceHandler: IRequestHandler<SetStudentAttendanceCommand, Result<Unit>>
{
    private readonly IAppUserResolver _userResolver;
    private readonly UserManager<AppUser> _userManager;
    private readonly IUnitOfWork _unitOfWork;   

    public SetStudentAttendanceHandler(IAppUserResolver userResolver, UserManager<AppUser> userManager, IUnitOfWork unitOfWork)
    {
        _userResolver = userResolver;
        _userManager = userManager;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Unit>> Handle(SetStudentAttendanceCommand request, CancellationToken cancellationToken)
    {
        var currentUser = await _userResolver.GetCurrentUserAsync(s => !s.IsDeleted,includes: new Func<IQueryable<AppUser>, IQueryable<AppUser>>[]{
            q => q.Include(p => p.Teacher).ThenInclude(p=>p.lessonStudentTeachers) });
        if(currentUser == null)
            return Result<Unit>.Failure(Error.Unauthorized, null,ErrorType.UnauthorizedError);
        var isUserTeacher  =await AuthExtension.CheckExistenceOfRoleInAppUser(_userManager, currentUser, "Teacher",null);
        if (!isUserTeacher)
        {
            return Result<Unit>.Failure(Error.Unauthorized, null,ErrorType.UnauthorizedError);
        }
        var existedStudent=await _unitOfWork.StudentRepository.GetEntity(s=>s.Id == request.StudentId);
        var existedLessonAssignment=await _unitOfWork.LessonUnitAssignmentRepository.GetEntity(s=>s.Id == request.LessonUnitAssignmentId);
        if (existedStudent == null || existedLessonAssignment == null)
        {
            return Result<Unit>.Failure(Error.NotFound, null, ErrorType.NotFoundError);
        }
        var isStudentInTeacherList=currentUser.Teacher.lessonStudentTeachers.Select(s=>s.Student).Any(s=>s.Id == request.StudentId);
        if (!isStudentInTeacherList)
            return Result<Unit>.Failure(Error.Custom("Student", "student is not one of the in the list of this teacher"),null, ErrorType.BusinessLogicError);
        var isLessonNotHeld = (DateTime.UtcNow < existedLessonAssignment.ScheduledStartTime);
        if (isLessonNotHeld)
        {
            return Result<Unit>.Failure(Error.Custom("error","lesson hasnt still started yet"), null, ErrorType.NotFoundError);
        }

        var newLessonAttendence = new LessonUnitAttendance()
        {
lessonUnitAssignmentId = request.LessonUnitAssignmentId ,
AttendanceDate = DateTime.UtcNow,
IsPresent = request.IsCompleted
        };
        await _unitOfWork.LessonUnitAttendanceRepository.Create(newLessonAttendence);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<Unit>.Success(Unit.Value,successReturnType:SuccessReturnType.Created);
    }
}