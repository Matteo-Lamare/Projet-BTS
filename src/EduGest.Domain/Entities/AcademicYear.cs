namespace EduGest.Domain.Entities;

public sealed class AcademicYear
{
    public Guid Id { get; set; }
    public string Label { get; set; } = string.Empty;
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
}
