namespace EduGest.Domain.Entities;
public sealed class Parent { public Guid Id { get; set; } public Guid UserId { get; set; } public string FirstName { get; set; } = string.Empty; public string LastName { get; set; } = string.Empty; }
