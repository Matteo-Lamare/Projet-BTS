using EduGest.Domain.Entities;
using EduGest.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EduGest.Infrastructure.Persistence;

public sealed class EduGestDbContext(DbContextOptions<EduGestDbContext> options)
    : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>(options)
{
    public DbSet<Permission> Permissions => Set<Permission>();
    public DbSet<Student> Students => Set<Student>();
    public DbSet<Teacher> Teachers => Set<Teacher>();
    public DbSet<Parent> Parents => Set<Parent>();
    public DbSet<Class> Classes => Set<Class>();
    public DbSet<Subject> Subjects => Set<Subject>();
    public DbSet<AcademicYear> AcademicYears => Set<AcademicYear>();
    public DbSet<Enrollment> Enrollments => Set<Enrollment>();
    public DbSet<TeachingAssignment> TeachingAssignments => Set<TeachingAssignment>();
    public DbSet<Assessment> Assessments => Set<Assessment>();
    public DbSet<Grade> Grades => Set<Grade>();
    public DbSet<Room> Rooms => Set<Room>();
    public DbSet<Session> Sessions => Set<Session>();
    public DbSet<AttendanceEvent> AttendanceEvents => Set<AttendanceEvent>();
    public DbSet<ParentStudent> ParentStudents => Set<ParentStudent>();
    public DbSet<UserRole> UserRoles => Set<UserRole>();
    public DbSet<RolePermission> RolePermissions => Set<RolePermission>();

    protected override void OnModelCreating(ModelBuilder m)
    {
        base.OnModelCreating(m);
        m.HasDefaultSchema("public");

        m.Entity<Permission>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).HasMaxLength(150).IsRequired();
            e.HasIndex(x => x.Name).IsUnique();
        });

        m.Entity<Student>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.FirstName).HasMaxLength(100).IsRequired();
            e.Property(x => x.LastName).HasMaxLength(100).IsRequired();
            e.HasIndex(x => x.UserId).IsUnique();
            e.HasOne<ApplicationUser>().WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
        });

        m.Entity<Teacher>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.FirstName).HasMaxLength(100).IsRequired();
            e.Property(x => x.LastName).HasMaxLength(100).IsRequired();
            e.HasIndex(x => x.UserId).IsUnique();
            e.HasOne<ApplicationUser>().WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
        });

        m.Entity<Parent>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.FirstName).HasMaxLength(100).IsRequired();
            e.Property(x => x.LastName).HasMaxLength(100).IsRequired();
            e.HasIndex(x => x.UserId).IsUnique();
            e.HasOne<ApplicationUser>().WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
        });

        m.Entity<AcademicYear>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Label).HasMaxLength(20).IsRequired();
        });

        m.Entity<Class>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).HasMaxLength(100).IsRequired();
            e.HasOne<AcademicYear>().WithMany().HasForeignKey(x => x.AcademicYearId).OnDelete(DeleteBehavior.Restrict);
            e.HasIndex(x => new { x.Name, x.AcademicYearId }).IsUnique();
        });

        m.Entity<Subject>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).HasMaxLength(150).IsRequired();
            e.Property(x => x.Code).HasMaxLength(30).IsRequired();
            e.HasIndex(x => x.Code).IsUnique();
        });

        m.Entity<Enrollment>(e =>
        {
            e.HasKey(x => x.Id);
            e.HasIndex(x => new { x.StudentId, x.ClassId }).IsUnique();
            e.HasOne<Student>().WithMany().HasForeignKey(x => x.StudentId).OnDelete(DeleteBehavior.Restrict);
            e.HasOne<Class>().WithMany().HasForeignKey(x => x.ClassId).OnDelete(DeleteBehavior.Restrict);
        });

        m.Entity<TeachingAssignment>(e =>
        {
            e.HasKey(x => x.Id);
            e.HasIndex(x => new { x.TeacherId, x.ClassId, x.SubjectId }).IsUnique();
            e.HasOne<Teacher>().WithMany().HasForeignKey(x => x.TeacherId).OnDelete(DeleteBehavior.Restrict);
            e.HasOne<Class>().WithMany().HasForeignKey(x => x.ClassId).OnDelete(DeleteBehavior.Restrict);
            e.HasOne<Subject>().WithMany().HasForeignKey(x => x.SubjectId).OnDelete(DeleteBehavior.Restrict);
        });

        m.Entity<Assessment>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Title).HasMaxLength(200).IsRequired();
            e.Property(x => x.MaxScore).HasPrecision(5, 2);
            e.HasOne<TeachingAssignment>().WithMany().HasForeignKey(x => x.TeachingAssignmentId).OnDelete(DeleteBehavior.Restrict);
        });

        m.Entity<Grade>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Score).HasPrecision(5, 2);
            e.Property(x => x.Comment).HasMaxLength(1000);
            e.HasIndex(x => new { x.AssessmentId, x.StudentId }).IsUnique();
            e.HasOne<Assessment>().WithMany().HasForeignKey(x => x.AssessmentId).OnDelete(DeleteBehavior.Restrict);
            e.HasOne<Student>().WithMany().HasForeignKey(x => x.StudentId).OnDelete(DeleteBehavior.Restrict);
        });

        m.Entity<Room>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).HasMaxLength(100).IsRequired();
            e.HasIndex(x => x.Name).IsUnique();
        });

        m.Entity<Session>(e =>
        {
            e.HasKey(x => x.Id);
            e.HasOne<TeachingAssignment>().WithMany().HasForeignKey(x => x.TeachingAssignmentId).OnDelete(DeleteBehavior.Restrict);
            e.HasOne<Room>().WithMany().HasForeignKey(x => x.RoomId).OnDelete(DeleteBehavior.SetNull);
        });

        m.Entity<AttendanceEvent>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Type).HasMaxLength(30).IsRequired();
            e.Property(x => x.Reason).HasMaxLength(1000);
            e.HasOne<Student>().WithMany().HasForeignKey(x => x.StudentId).OnDelete(DeleteBehavior.Restrict);
            e.HasOne<Session>().WithMany().HasForeignKey(x => x.SessionId).OnDelete(DeleteBehavior.SetNull);
        });

        m.Entity<ParentStudent>(e =>
        {
            e.HasKey(x => new { x.ParentId, x.StudentId });
            e.HasOne<Parent>().WithMany().HasForeignKey(x => x.ParentId).OnDelete(DeleteBehavior.Cascade);
            e.HasOne<Student>().WithMany().HasForeignKey(x => x.StudentId).OnDelete(DeleteBehavior.Cascade);
        });

        m.Entity<UserRole>(e =>
        {
            e.HasKey(x => new { x.UserId, x.RoleId });
            e.HasOne<ApplicationUser>().WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
            e.HasOne<ApplicationRole>().WithMany().HasForeignKey(x => x.RoleId).OnDelete(DeleteBehavior.Cascade);
        });

        m.Entity<RolePermission>(e =>
        {
            e.HasKey(x => new { x.RoleId, x.PermissionId });
            e.HasOne<ApplicationRole>().WithMany().HasForeignKey(x => x.RoleId).OnDelete(DeleteBehavior.Cascade);
            e.HasOne<Permission>().WithMany().HasForeignKey(x => x.PermissionId).OnDelete(DeleteBehavior.Cascade);
        });
    }
}
