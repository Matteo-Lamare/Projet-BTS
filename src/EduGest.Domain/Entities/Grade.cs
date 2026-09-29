namespace EduGest.Domain.Entities;

public sealed class Grade
{
    public Guid Id { get; set; }
    public Guid AssessmentId { get; set; }
    public Guid StudentId { get; set; }
    public decimal Score { get; set; }
    public string? Comment { get; set; }
}
