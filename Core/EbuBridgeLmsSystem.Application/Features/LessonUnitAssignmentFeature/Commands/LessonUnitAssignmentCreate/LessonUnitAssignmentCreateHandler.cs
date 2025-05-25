using EbuBridgeLmsSystem.Application.Interfaces;
using EbuBridgeLmsSystem.Domain.Entities.Common;
using EbuBridgeLmsSystem.Domain.Entities.LmsSystem;
using EbuBridgeLmsSystem.Domain.Repositories;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EbuBridgeLmsSystem.Application.Features.LessonUnitAssignmentFeature.Commands.LessonUnitAssignmentCreate
{
    public class LessonUnitAssignmentCreateHandler : IRequestHandler<LessonUnitAssignmentCreateCommand, Result<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppUserResolver _appUserResolver;
        private static readonly int  TimeGap=10;
        
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
            
            var isLessonTimeAlreadyTaken = curentUserInTheSystem.Teacher.lessonUnitAssignments
     .Any(s => s.ScheduledStartTime < request.ScheduledEndTime
            && s.ScheduledEndTime>request.ScheduledStartTime);
            if (isLessonTimeAlreadyTaken)
            {
                return Result<Unit>.Failure(Error.Custom("Error", "lesson time for teacher already taken"), null, ErrorType.BusinessLogicError);
            }
            var latestAssignment = await _unitOfWork.LessonUnitAssignmentRepository.GetLatestUnitAssignment();
            if ((request.ScheduledStartTime - latestAssignment.ScheduledEndTime).TotalMinutes <= TimeGap)
            {
                return Result<Unit>.Failure(Error.Custom("error","there is gotta be gap time between lessons"), null, ErrorType.BusinessLogicError);
            }
            //string roomName = GenerateRoomName();
            //string meetingUrl = $"{meetingUrlDefaultLink}{roomName}";
            var newLessonUnitAssignment = new LessonUnitAssignment()
            {
                ScheduledEndTime = request.ScheduledEndTime,
                ScheduledStartTime = request.ScheduledStartTime,
                StudentId = request.StudentId,
                TeacherId = request.TeacherId,
                Duration = TimeSpan.Zero,
                LessonUnitId = request.LessonUnitId,
                LessonMeeting=null,
            };
            newLessonUnitAssignment.SetDurationAutomaticly();
            await _unitOfWork.LessonUnitAssignmentRepository.Create(newLessonUnitAssignment);
            await _unitOfWork.SaveChangesAsync();
            return Result<Unit>.Success(Unit.Value, successReturnType: SuccessReturnType.Created);
        }
      
    }
}
