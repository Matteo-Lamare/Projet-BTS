namespace EduGest.Domain.Entities;

public sealed class Enrollment
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public Guid ClassId { get; set; }
    public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;
}
