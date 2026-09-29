namespace EduGest.Infrastructure.Identity;

public sealed class AdminBootstrapOptions
{
    public const string SectionName = "AdminBootstrap";
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
}
