using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EbuBridgeLmsSystem.Api.App.Common
{
    [Area("Admin")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class BaseAdminController : ControllerBase
    {
    }
}
