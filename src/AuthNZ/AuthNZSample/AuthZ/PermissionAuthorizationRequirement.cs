using Microsoft.AspNetCore.Authorization;

namespace AuthZ;

public class PermissionAuthorizationRequirement : IAuthorizationRequirement
{
    public PermissionAuthorizationRequirement(string name)
    {
        Name = name;
    }
    
    public string Name { get; private set; }
}