using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Features.LessonStudentFeature.LessonStudentApproval
{
     public sealed record LessonStudentApprovalCommand:IRequest<Result<Unit>>
    {
        public Guid LessonStudentId { get; set; }
    }
}
