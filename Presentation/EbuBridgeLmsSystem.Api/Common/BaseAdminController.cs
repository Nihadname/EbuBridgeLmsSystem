﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EbuBridgeLmsSystem.Api.Common
{
    [Area("Admin")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public abstract class BaseAdminController : ControllerBase
    {
    }
}
