using EduGest.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EduGest.Infrastructure.Persistence;

public static class DatabaseInitializer
{
    public static async Task InitializeAsync(IServiceProvider services, IConfiguration configuration, bool migrate = true, CancellationToken cancellationToken = default)
    {
        using var scope = services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<EduGestDbContext>();
        var roles = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
        var users = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        if (migrate) await db.Database.MigrateAsync(cancellationToken);
        await IdentitySeed.SeedAsync(db, roles, migrate: false, cancellationToken);

        var options = configuration.GetSection(AdminBootstrapOptions.SectionName).Get<AdminBootstrapOptions>();
        if (options is null || string.IsNullOrWhiteSpace(options.UserName) || string.IsNullOrWhiteSpace(options.Password)) return;

        var admin = await users.FindByNameAsync(options.UserName);
        if (admin is null)
        {
            admin = new ApplicationUser { Id = Guid.NewGuid(), UserName = options.UserName, Email = options.Email, EmailConfirmed = true };
            var created = await users.CreateAsync(admin, options.Password);
            if (!created.Succeeded) throw new InvalidOperationException("Impossible de créer le premier administrateur : " + string.Join("; ", created.Errors.Select(x => x.Description)));
        }

        if (!await users.IsInRoleAsync(admin, "Administrator"))
            await users.AddToRoleAsync(admin, "Administrator");
    }
}
