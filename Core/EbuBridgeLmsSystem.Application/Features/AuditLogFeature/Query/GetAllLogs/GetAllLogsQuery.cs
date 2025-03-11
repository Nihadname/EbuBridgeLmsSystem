using EbuBridgeLmsSystem.Domain.Entities.Common;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Features.AuditLogFeature.Query.GetAllLogs
{
    public class GetAllLogsQuery:IRequest<Result<PaginatedResult<AuditLogListItemDto>>>
    {
        public string Cursor { get; init; }
        public int Limit { get; init; }
        public string TableNameSearchQuery { get; init; }
        public string ActionSearchQuery { get; init; }
        public string UserId { get; init; }

    }
}
