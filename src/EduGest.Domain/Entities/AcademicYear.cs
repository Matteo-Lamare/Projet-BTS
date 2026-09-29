namespace EduGest.Domain.Entities;
public sealed class AcademicYear { public Guid Id { get; set; } public string Label { get; set; } = string.Empty; public DateTime StartDate { get; set; } public DateTime EndDate { get; set; } }
