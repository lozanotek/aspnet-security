using System.Security.Claims;

namespace AuthZ;

public class AuthorizationStore 
{
    public SubjectAuthorizationResult? GetSubjectAuthorization(ClaimsPrincipal principal)
    {
        var subject = principal.Subject();
        if (string.IsNullOrWhiteSpace(subject))
        {
            return null;
        }

        var appRoles = AppRolesForSubject(subject);
        var permissions = PermissionsForSubject(subject, appRoles);

        return new SubjectAuthorizationResult
        {
            Subject = subject,
            Permissions = permissions,
            AppRoles = appRoles
        };
    }

    private string[]? AppRolesForSubject(string subject)
    {
        var appRoles = Database.AppRoles;

        var rolesBySubject = appRoles.Where(r =>
        {
            var subjects = r.Subjects;
            return subjects != null && subjects.Length != 0 && subjects.Contains(subject);
        }).Select(r => r.Name).ToArray();

        return rolesBySubject;
    }
    
    private string[] PermissionsForSubject(string subject, string[] appRolesForSubject)
    {
        var permissions = Database.Permissions;

        var permsBySubject = permissions.Where(p =>
        {
            var subjects = p.Subjects;
            return subjects != null && subject.Length != 0 && subjects.Contains(subject);
        }).Select(p => p.Name).ToArray();

        var subjectPerms = new List<string>();
        subjectPerms.AddRange(permsBySubject);

        var appRoles = Database.AppRoles;
        if (appRoles.Length == 0)
        {
            return subjectPerms.Distinct().ToArray();
        }

        var permByAppRole = permissions.Where(p =>
        {
            var permAppRoles = p.AppRoles;
            return permAppRoles != null && permAppRoles.Length != 0 && permAppRoles.Any(appRolesForSubject.Contains);
        }).Select(r => r.Name).ToArray();
        
        subjectPerms.AddRange(permByAppRole);
        return subjectPerms.Distinct().ToArray();
    }
}