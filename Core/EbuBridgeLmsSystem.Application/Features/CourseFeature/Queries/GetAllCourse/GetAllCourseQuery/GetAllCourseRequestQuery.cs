using EbuBridgeLmsSystem.Domain.Entities.Common;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EbuBridgeLmsSystem.Application.Features.CourseFeature.Queries.GetAllCourse.GetAllCourseQuery
{
    public sealed record GetAllCourseRequestQuery:IRequest<Result<PaginatedResult<CourseListItemDto>>>
    {
        public string Cursor { get; init; }
        public int Limit { get; init; }
        public string SearchQuery { get; init; }
    }
}
