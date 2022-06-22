namespace AuthZ;

public class SubjectAuthorizationResult
{
    public string? Subject { get; set; }
    public string[]? AppRoles { get; set; }
    public string[]? Permissions { get; set; }
}