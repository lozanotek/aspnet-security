using AuthZ;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace Microsoft.Extensions.DependencyInjection;

public static class AuthZServiceCollectionExtensions
{
    public static IServiceCollection AddPermissions(this IServiceCollection services)
    {
        // Need middleware to wire all the auth pieces
        services.AddAuthorization();
        
        services.AddSingleton<AuthorizationStore>();
        services.AddTransient<IClaimsTransformation, PermissionClaimsTransformation>();
        services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
        services.AddTransient<IAuthorizationHandler, PermissionRequirementAuthorizationHandler>();

        return services;
    }
}