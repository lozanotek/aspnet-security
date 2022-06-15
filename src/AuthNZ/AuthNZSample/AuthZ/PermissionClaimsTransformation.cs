using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace AuthZ;

public class PermissionClaimsTransformation : IClaimsTransformation
{
    private readonly AuthorizationStore authorizationStore;

    public PermissionClaimsTransformation(AuthorizationStore authorizationStore)
    {
        this.authorizationStore = authorizationStore;
    }
    
    public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        var authResult = authorizationStore.GetSubjectAuthorization(principal);
        if (authResult == null)
        {
            return Task.FromResult(principal);
        }

        var appRoleClaims = authResult?.AppRoles?.Select(r => new Claim(PermissionClaimsConstants.AppRole, r)).ToArray();
        var permClaims = authResult?.Permissions?.Select(p => new Claim(PermissionClaimsConstants.Permission, p)).ToArray();

        var claimList = new List<Claim>();
        if (appRoleClaims != null)
        {
            claimList.AddRange(appRoleClaims);
        }

        if (permClaims != null)
        {
            claimList.AddRange(permClaims);
        }

        var permIdentity = new ClaimsIdentity("AuthZ", "name", "appRole");
        permIdentity.AddClaims(claimList.ToArray());
        principal.AddIdentity(permIdentity);

        return Task.FromResult(principal);
    }
}