using EbuBridgeLmsSystem.Api.Extensions;
using EbuBridgeLmsSystem.Application.Features.CourseFeature.Queries.GetAllCourse.GetAllCourseQuery;
using EbuBridgeLmsSystem.Application.Features.CourseFeature.Queries.GetByIdCourse;
using EbuBridgeLmsSystem.Domain.Entities.Common;
using LearningManagementSystem.Core.Entities.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EbuBridgeLmsSystem.Api.App.Admin
{
    [Area("Admin")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CourseController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("GetAllWithPaganation")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll(string? cursor = null, string? searchQuery = null, int limit = 4)
        {
            var cityGetAllQuery = new GetAllCourseRequestQuery()
            {
                Cursor = cursor,
                SearchQuery = searchQuery,
                Limit = limit
            };
            var result = await _mediator.Send(cityGetAllQuery);
            return this.ToActionResult(result);
        }
        [HttpGet("{Id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById(Guid Id)
        {
            if(Id == Guid.Empty)
            {
                var errorResult = Result<Unit>.Failure(Error.ValidationFailed, null, ErrorType.ValidationError);
                return this.ToActionResult(errorResult);
            }
            var getByIdQuery = new GetByIdCourseQuery()
            {
                Id = Id
            };
            var result = await _mediator.Send(getByIdQuery);
            return this.ToActionResult(result);
        } 
    }
}
