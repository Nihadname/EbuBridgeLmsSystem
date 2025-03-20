using EbuBridgeLmsSystem.Domain.Entities.Common;
using EbuBridgeLmsSystem.Domain.Repositories;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Features.CourseFeature.Commands.UpdateCourse
{
    public class UpdateCourseHandler : IRequestHandler<UpdateCourseCommand, Result<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public async Task<Result<Unit>> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            var existedCourse=await _unitOfWork.CourseRepository.GetEntity(s=>s.Id==request.CourseId&!s.IsDeleted);
            if (existedCourse == null)
                return Result<Unit>.Failure(Error.NotFound, null, ErrorType.NotFoundError);
            bool hasChanges=false;
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                var ExistedCourseWithThisName = await _unitOfWork.CourseRepository.GetEntity(s => EF.Functions.Like(s.Name, $"%{request.Name}%")&&s.Id!=request.CourseId,AsnoTracking:true);
                if(ExistedCourseWithThisName is not null)
                {
                    if (ExistedCourseWithThisName.IsDeleted)
                    {
                        return Result<Unit>.Failure(Error.Custom("Course Name", "This Course was previously deleted. Consider renaming the existing record instead of restoring."), null, ErrorType.BusinessLogicError);
                    }
                    return Result<Unit>.Failure(Error.Custom("Course Name", "it already exists"), null, ErrorType.BusinessLogicError);
                }
                existedCourse.Name = request.Name;
                hasChanges=true;
            }

            throw new NotImplementedException();
        }
    }
}
