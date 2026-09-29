using EduGest.Infrastructure.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduGest.Api.Controllers;

[ApiController]
[Route("api/authorization")]
public sealed class AuthorizationProbeController : ControllerBase
{
    [HttpGet("users-manage")]
    [Authorize(Policy = "Permission:users.manage")]
    public IActionResult UsersManage() => NoContent();
}
