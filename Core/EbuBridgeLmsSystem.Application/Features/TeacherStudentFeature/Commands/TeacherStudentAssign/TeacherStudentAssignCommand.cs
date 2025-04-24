using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Features.TeacherStudentFeature.Commands.TeacherStudentAssign
{
    public sealed record TeacherStudentAssignCommand:IRequest<Result<Unit>>
    {
        public Guid TeacherId { get; init; }
        public Guid StudentId { get; init; }
    }
}
