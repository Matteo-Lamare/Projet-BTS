namespace EduGest.Domain.Entities;

public sealed class ParentStudent
{
    public Guid ParentId { get; set; }
    public Guid StudentId { get; set; }
}
