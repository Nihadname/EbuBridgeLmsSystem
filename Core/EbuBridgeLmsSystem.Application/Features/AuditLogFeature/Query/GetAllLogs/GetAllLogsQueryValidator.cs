using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Features.AuditLogFeature.Query.GetAllLogs
{
    public class GetAllLogsQueryValidator : AbstractValidator<GetAllLogsQuery>
    {
        public GetAllLogsQueryValidator()
        {
            RuleFor(s => s.Limit).NotNull();
        }
    }
}
