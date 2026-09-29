using System.Security.Claims;
using EduGest.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EduGest.Api.Controllers;

[ApiController]
[Route("api/auth")]
public sealed class AuthController(UserManager<ApplicationUser> userManager, TokenService tokens) : ControllerBase
{
    public sealed record LoginRequest(string UserName, string Password);
    public sealed record RefreshRequest(string RefreshToken);
    public sealed record TokenResponse(string AccessToken, string RefreshToken);

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<TokenResponse>> Login(LoginRequest request, CancellationToken ct)
    {
        var user = await userManager.FindByNameAsync(request.UserName);
        if (user is null || !await userManager.CheckPasswordAsync(user, request.Password)) return Unauthorized();
        var pair = await tokens.CreateAsync(user, ct);
        return Ok(new TokenResponse(pair.AccessToken, pair.RefreshToken));
    }

    [HttpPost("refresh")]
    [AllowAnonymous]
    public async Task<ActionResult<TokenResponse>> Refresh(RefreshRequest request, CancellationToken ct)
    {
        var result = await tokens.RotateAsync(request.RefreshToken, ct);
        return result is null ? Unauthorized() : Ok(new TokenResponse(result.Value.AccessToken, result.Value.RefreshToken));
    }

    [HttpPost("logout")]
    [AllowAnonymous]
    public async Task<IActionResult> Logout(RefreshRequest request, CancellationToken ct)
    {
        await tokens.RevokeAsync(request.RefreshToken, ct);
        return NoContent();
    }

    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> Me()
    {
        var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = id is null ? null : await userManager.FindByIdAsync(id);
        if (user is null) return Unauthorized();
        var roles = await userManager.GetRolesAsync(user);
        return Ok(new { user.Id, user.UserName, user.Email, Roles = roles });
    }
}
