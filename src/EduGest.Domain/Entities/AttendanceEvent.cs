namespace EduGest.Domain.Entities;

public sealed class AttendanceEvent
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public Guid? SessionId { get; set; }
    public DateTime OccurredAt { get; set; }
    public string Type { get; set; } = "ABSENCE";
    public string? Reason { get; set; }
}
