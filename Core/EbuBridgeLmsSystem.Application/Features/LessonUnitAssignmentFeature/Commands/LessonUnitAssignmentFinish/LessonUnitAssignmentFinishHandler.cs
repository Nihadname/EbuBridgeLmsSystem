using EbuBridgeLmsSystem.Application.Helpers.Extensions.Auth;
using EbuBridgeLmsSystem.Application.Interfaces;
using EbuBridgeLmsSystem.Domain.Entities.Common;
using EbuBridgeLmsSystem.Domain.Entities.LmsSystem;
using EbuBridgeLmsSystem.Domain.Repositories;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EbuBridgeLmsSystem.Application.Features.LessonUnitAssignmentFeature.Commands.LessonUnitAssignmentFinish;

public sealed class LessonUnitAssignmentFinishHandler:IRequestHandler<LessonUnitAssignmentFinishCommand, Result<Unit>>
{
    private readonly ILogger<LessonUnitAssignmentFinishHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<AppUser> _userManager;
    private readonly IAppUserResolver _userResolver;

    public LessonUnitAssignmentFinishHandler(ILogger<LessonUnitAssignmentFinishHandler> logger,
        IUnitOfWork unitOfWork, UserManager<AppUser> userManager, IAppUserResolver userResolver)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _userResolver = userResolver;
    }

    public async Task<Result<Unit>> Handle(LessonUnitAssignmentFinishCommand request, CancellationToken cancellationToken)
    {
        var currentUser = await _userResolver.GetCurrentUserAsync(s=>!s.IsDeleted);
        if(currentUser == null)
            return Result<Unit>.Failure(Error.Unauthorized, null,ErrorType.UnauthorizedError);
        var isUserTeacher  =await AuthExtension.CheckExistenceOfRoleInAppUser(_userManager, currentUser, "Teacher",null);
        if (!isUserTeacher)
        {
            return Result<Unit>.Failure(Error.Unauthorized, null,ErrorType.UnauthorizedError);
        }
        var existedLessonUnitAssignment =
            await _unitOfWork.LessonUnitAssignmentRepository.GetEntity(s => s.Id == request.LessonUnitAssignmentId,
                includes: new Func<IQueryable<LessonUnitAssignment>, IQueryable<LessonUnitAssignment>>[]{
                    q => q.Include(p => p.lessonUnitAttendance)});
        if (existedLessonUnitAssignment is null || 
            existedLessonUnitAssignment.isLessonFinished == true||existedLessonUnitAssignment.lessonUnitAttendance is null)
        {
            return Result<Unit>.Failure(Error.NotFound, null,ErrorType.NotFoundError);
        }
        existedLessonUnitAssignment.isLessonFinished = true;
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<Unit>.Success(Unit.Value,SuccessReturnType.NoContent);
    }
}