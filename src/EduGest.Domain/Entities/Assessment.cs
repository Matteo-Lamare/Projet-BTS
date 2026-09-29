namespace EduGest.Domain.Entities;

public sealed class Assessment
{
    public Guid Id { get; set; }
    public Guid TeachingAssignmentId { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime AssessmentDate { get; set; }
    public decimal MaxScore { get; set; } = 20m;
}
