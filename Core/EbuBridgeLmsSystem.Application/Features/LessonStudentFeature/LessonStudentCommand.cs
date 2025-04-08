using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Features.LessonStudentFeature
{
    public sealed record LessonStudentCommand:IRequest<Result<Unit>>
    {
    }
}
