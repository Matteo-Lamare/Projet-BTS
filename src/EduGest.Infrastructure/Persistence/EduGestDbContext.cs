using Microsoft.EntityFrameworkCore;

namespace EduGest.Infrastructure.Persistence;

public sealed class EduGestDbContext(DbContextOptions<EduGestDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("public");
    }
}
