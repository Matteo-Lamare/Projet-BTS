using EduGest.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EduGest.Infrastructure.Persistence;

public sealed class EduGestDbContext(DbContextOptions<EduGestDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<Permission> Permissions => Set<Permission>();
    public DbSet<Student> Students => Set<Student>();
    public DbSet<Teacher> Teachers => Set<Teacher>();
    public DbSet<Parent> Parents => Set<Parent>();
    public DbSet<Class> Classes => Set<Class>();
    public DbSet<Subject> Subjects => Set<Subject>();
    public DbSet<AcademicYear> AcademicYears => Set<AcademicYear>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("public");

        modelBuilder.Entity<User>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.UserName).HasMaxLength(100).IsRequired();
            e.Property(x => x.Email).HasMaxLength(255).IsRequired();
            e.HasIndex(x => x.UserName).IsUnique();
            e.HasIndex(x => x.Email).IsUnique();
        });

        modelBuilder.Entity<Role>(e => { e.HasKey(x => x.Id); e.Property(x => x.Name).HasMaxLength(100).IsRequired(); e.HasIndex(x => x.Name).IsUnique(); });
        modelBuilder.Entity<Permission>(e => { e.HasKey(x => x.Id); e.Property(x => x.Name).HasMaxLength(150).IsRequired(); e.HasIndex(x => x.Name).IsUnique(); });

        modelBuilder.Entity<Student>(e => { e.HasKey(x => x.Id); e.Property(x => x.FirstName).HasMaxLength(100).IsRequired(); e.Property(x => x.LastName).HasMaxLength(100).IsRequired(); e.HasOne<User>().WithOne().HasForeignKey<Student>(x => x.UserId).OnDelete(DeleteBehavior.Restrict); });
        modelBuilder.Entity<Teacher>(e => { e.HasKey(x => x.Id); e.Property(x => x.FirstName).HasMaxLength(100).IsRequired(); e.Property(x => x.LastName).HasMaxLength(100).IsRequired(); e.HasOne<User>().WithOne().HasForeignKey<Teacher>(x => x.UserId).OnDelete(DeleteBehavior.Restrict); });
        modelBuilder.Entity<Parent>(e => { e.HasKey(x => x.Id); e.Property(x => x.FirstName).HasMaxLength(100).IsRequired(); e.Property(x => x.LastName).HasMaxLength(100).IsRequired(); e.HasOne<User>().WithOne().HasForeignKey<Parent>(x => x.UserId).OnDelete(DeleteBehavior.Restrict); });
        modelBuilder.Entity<AcademicYear>(e => { e.HasKey(x => x.Id); e.Property(x => x.Label).HasMaxLength(20).IsRequired(); });
        modelBuilder.Entity<Class>(e => { e.HasKey(x => x.Id); e.Property(x => x.Name).HasMaxLength(100).IsRequired(); e.HasOne<AcademicYear>().WithMany().HasForeignKey(x => x.AcademicYearId).OnDelete(DeleteBehavior.Restrict); });
        modelBuilder.Entity<Subject>(e => { e.HasKey(x => x.Id); e.Property(x => x.Name).HasMaxLength(150).IsRequired(); e.Property(x => x.Code).HasMaxLength(30).IsRequired(); e.HasIndex(x => x.Code).IsUnique(); });
    }
}
