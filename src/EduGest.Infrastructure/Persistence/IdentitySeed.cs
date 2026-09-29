using EduGest.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EduGest.Infrastructure.Persistence;

public static class IdentitySeed
{
    private static readonly string[] Roles = ["Student", "Teacher", "Parent", "Administration", "Administrator"];
    private static readonly string[] Permissions = [
        "students.read", "students.write", "teachers.read", "teachers.write", "classes.read", "classes.write",
        "subjects.read", "subjects.write", "grades.read", "grades.write", "attendance.read", "attendance.write",
        "documents.read", "documents.write", "messages.read", "messages.write", "timetable.read",
        "statistics.read", "users.manage", "roles.manage", "audit.read", "settings.manage"
    ];

    public static async Task SeedAsync(EduGestDbContext db, RoleManager<ApplicationRole> roleManager, CancellationToken cancellationToken = default)
    {
        await db.Database.MigrateAsync(cancellationToken);

        foreach (var roleName in Roles)
            if (!await roleManager.RoleExistsAsync(roleName))
                await roleManager.CreateAsync(new ApplicationRole { Id = DeterministicGuid("role:" + roleName), Name = roleName });

        var existing = await db.Permissions.Select(x => x.Name).ToListAsync(cancellationToken);
        var permissions = Permissions.Where(x => !existing.Contains(x)).Select(x => new Domain.Entities.Permission { Id = DeterministicGuid("permission:" + x), Name = x });
        db.Permissions.AddRange(permissions);
        await db.SaveChangesAsync(cancellationToken);
    }

    private static Guid DeterministicGuid(string value)
    {
        var bytes = System.Security.Cryptography.SHA256.HashData(System.Text.Encoding.UTF8.GetBytes(value));
        return new Guid(bytes[..16]);
    }
}
