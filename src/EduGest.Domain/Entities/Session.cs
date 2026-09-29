namespace EduGest.Domain.Entities;

public sealed class Session
{
    public Guid Id { get; set; }
    public Guid TeachingAssignmentId { get; set; }
    public Guid? RoomId { get; set; }
    public DateTime StartsAt { get; set; }
    public DateTime EndsAt { get; set; }
}
