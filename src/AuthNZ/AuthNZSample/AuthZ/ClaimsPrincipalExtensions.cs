using System.Security.Claims;

namespace AuthZ;

public static class ClaimsPrincipalExtensions
{
    public static string Subject(this ClaimsPrincipal principal)
    {
        return principal.FindFirstValue("sub");
    }
    
    public static IEnumerable<string> AppRoles(this ClaimsPrincipal principal)
    {
        var appRoles = principal.FindAll(PermissionClaimsConstants.AppRole).Select(x => x.Value);
        return appRoles.ToArray();
    }
    
    public static IEnumerable<string> Permissions(this ClaimsPrincipal principal)
    {
        var permissions = principal.FindAll(PermissionClaimsConstants.Permission).Select(x => x.Value);
        return permissions.ToArray();
    }

    public static bool HasPermission(this ClaimsPrincipal principal, string permission)
    {
        var hasPermission = principal.HasClaim(claim => 
            string.Equals(claim.Type, PermissionClaimsConstants.Permission) &&
            string.Equals(claim.Value, permission, StringComparison.CurrentCultureIgnoreCase));

        return hasPermission;
    }
    
    public static bool HasAppRole(this ClaimsPrincipal principal, string appRole)
    {
        var hasAppRole = principal.HasClaim(claim => 
            string.Equals(claim.Type, PermissionClaimsConstants.AppRole) &&
            string.Equals(claim.Value, appRole, StringComparison.CurrentCultureIgnoreCase));

        return hasAppRole;
    }
}