namespace EduGest.Domain.Entities;

public sealed class Class
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid AcademicYearId { get; set; }
}
