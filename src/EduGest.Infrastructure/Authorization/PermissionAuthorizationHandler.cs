using System.Security.Claims;
using EduGest.Infrastructure.Identity;
using EduGest.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EduGest.Infrastructure.Authorization;

public sealed class PermissionAuthorizationHandler(UserManager<ApplicationUser> userManager, EduGestDbContext db)
    : AuthorizationHandler<PermissionRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        var id = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!Guid.TryParse(id, out var userId)) return;
        var user = await userManager.FindByIdAsync(userId.ToString());
        if (user is null) return;
        var roleNames = await userManager.GetRolesAsync(user);
        var roleIds = await db.Roles.Where(x => x.Name != null && roleNames.Contains(x.Name)).Select(x => x.Id).ToListAsync();
        var allowed = await db.RolePermissions.Where(x => roleIds.Contains(x.RoleId))
            .Join(db.Permissions, rp => rp.PermissionId, p => p.Id, (_, p) => p.Name)
            .AnyAsync(x => x == requirement.Permission);
        if (allowed) context.Succeed(requirement);
    }
}
