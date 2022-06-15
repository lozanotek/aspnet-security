using Microsoft.AspNetCore.Authorization;

namespace AuthZ;

public class PermissionRequirementAuthorizationHandler : AuthorizationHandler<PermissionAuthorizationRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionAuthorizationRequirement authorizationRequirement)
    {
        var permission = authorizationRequirement.Name;
        var hasPermission = context.User.HasPermission(permission);
        if (hasPermission)
        {
            context.Succeed(authorizationRequirement);
        }

        return Task.CompletedTask;
    }
}