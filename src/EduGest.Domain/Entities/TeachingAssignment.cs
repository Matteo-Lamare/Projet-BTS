namespace EduGest.Domain.Entities;

public sealed class TeachingAssignment
{
    public Guid Id { get; set; }
    public Guid TeacherId { get; set; }
    public Guid ClassId { get; set; }
    public Guid SubjectId { get; set; }
}
