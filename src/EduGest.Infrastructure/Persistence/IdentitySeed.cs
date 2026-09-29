using EduGest.Domain.Entities;
using EduGest.Infrastructure.Authorization;
using EduGest.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EduGest.Infrastructure.Persistence;

public static class IdentitySeed
{
    private static readonly string[] Roles = ["Student", "Teacher", "Parent", "Administration", "Administrator"];

    private static readonly Dictionary<string, string[]> RolePermissions = new()
    {
        ["Student"] = ["grades.read", "attendance.read", "documents.read", "messages.read", "messages.write", "timetable.read"],
        ["Parent"] = ["grades.read", "attendance.read", "documents.read", "timetable.read"],
        ["Teacher"] = ["students.read", "classes.read", "subjects.read", "grades.read", "grades.write", "attendance.read", "attendance.write", "documents.read", "documents.write", "messages.read", "messages.write", "timetable.read"],
        ["Administration"] = ["students.read", "students.write", "teachers.read", "teachers.write", "classes.read", "classes.write", "subjects.read", "subjects.write", "grades.read", "attendance.read", "documents.read", "documents.write", "statistics.read", "timetable.read"],
        ["Administrator"] = Permissions.All
    };

    public static async Task SeedAsync(EduGestDbContext db, RoleManager<ApplicationRole> roleManager, CancellationToken cancellationToken = default)
    {
        await db.Database.MigrateAsync(cancellationToken);
        foreach (var roleName in Roles)
            if (!await roleManager.RoleExistsAsync(roleName))
                await roleManager.CreateAsync(new ApplicationRole { Id = DeterministicGuid("role:" + roleName), Name = roleName });

        var existing = await db.Permissions.ToDictionaryAsync(x => x.Name, cancellationToken);
        foreach (var name in Permissions.All)
            if (!existing.ContainsKey(name))
                db.Permissions.Add(new Permission { Id = DeterministicGuid("permission:" + name), Name = name });
        await db.SaveChangesAsync(cancellationToken);

        var permissions = await db.Permissions.ToDictionaryAsync(x => x.Name, cancellationToken);
        var roles = await db.Roles.Where(x => x.Name != null).ToDictionaryAsync(x => x.Name!, cancellationToken);
        foreach (var (roleName, names) in RolePermissions)
            foreach (var name in names)
                if (!await db.RolePermissions.AnyAsync(x => x.RoleId == roles[roleName].Id && x.PermissionId == permissions[name].Id, cancellationToken))
                    db.RolePermissions.Add(new RolePermission { RoleId = roles[roleName].Id, PermissionId = permissions[name].Id });
        await db.SaveChangesAsync(cancellationToken);
    }

    private static Guid DeterministicGuid(string value)
    {
        var bytes = System.Security.Cryptography.SHA256.HashData(System.Text.Encoding.UTF8.GetBytes(value));
        return new Guid(bytes[..16]);
    }
}
