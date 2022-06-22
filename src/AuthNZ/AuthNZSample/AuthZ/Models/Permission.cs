namespace AuthZ.Models;

public class Permission
{
    public  string? Name { get; set; }
    public  string[]? AppRoles { get; set; }
    public  string[]? Subjects { get; set; }
}