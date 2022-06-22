using Microsoft.AspNetCore.Authorization;

namespace AuthZ;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class RequirePermissionAttribute : AuthorizeAttribute
{
    public RequirePermissionAttribute(string permission) : base(permission)
    {
    }
}