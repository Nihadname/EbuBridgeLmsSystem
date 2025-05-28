using EbuBridgeLmsSystem.Application.Helpers.Extensions.Auth;
using EbuBridgeLmsSystem.Application.Interfaces;
using EbuBridgeLmsSystem.Domain.Entities.Common;
using EbuBridgeLmsSystem.Domain.Enums;
using EbuBridgeLmsSystem.Domain.Repositories;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EbuBridgeLmsSystem.Application.Features.LessonUnitAssignmentFeature.Commands.LessonUnitAssignmentMeetingVerify;

public sealed class
    LessonUnitAssignmentMeetingVerifyHandler : IRequestHandler<LessonUnitAssignmentMeetingVerifyCommand, Result<Unit>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppUserResolver _userResolver;
    private readonly UserManager<AppUser> _userManager;

    public LessonUnitAssignmentMeetingVerifyHandler(IUnitOfWork unitOfWork, IAppUserResolver userResolver, UserManager<AppUser> userManager)
    {
        _unitOfWork = unitOfWork;
        _userResolver = userResolver;
        _userManager = userManager;
    }

    public async Task<Result<Unit>> Handle(LessonUnitAssignmentMeetingVerifyCommand request,
        CancellationToken cancellationToken)
    {
        var currentUser = await _userResolver.GetCurrentUserAsync();
        if(currentUser == null)
            return Result<Unit>.Failure(Error.Unauthorized, null,ErrorType.UnauthorizedError);
        var isUserTeacher  =await AuthExtension.CheckExistenceOfRoleInAppUser(_userManager, currentUser, "Teacher",null);
        if (!isUserTeacher)
        {
            return Result<Unit>.Failure(Error.Unauthorized, null,ErrorType.UnauthorizedError);
        }
        var existedLessonUnitAssignment =
            await _unitOfWork.LessonUnitAssignmentRepository.GetEntity(s => s.Id == request.LessonUnitAssignmentId);
        if (existedLessonUnitAssignment is null || existedLessonUnitAssignment?.LessonMeeting == null||
            existedLessonUnitAssignment.LessonMeeting.IsVerified==true)
        {
         return Result<Unit>.Failure(Error.NotFound, null,ErrorType.NotFoundError);
        }
        existedLessonUnitAssignment.LessonMeeting.IsVerified = true;
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<Unit>.Success(Unit.Value,SuccessReturnType.NoContent);
    }
   

}