using AuthZ.Models;

namespace AuthZ;

public static class Database
{
    public static readonly Permission[] Permissions = new[]
    {
        new Permission
        {
            Name = "CanEdit",
            AppRoles = new[]
            {
                "Editor",
                "Admin"
            }
        },
        new Permission
        {
            Name = "CanSubmit",
            AppRoles = new[]
            {
                "Admin"
            }
        },
        new Permission
        {
            Name = "CanRead",
            AppRoles = new[]
            {
                "Admin",
                "Editor",
                "Reader"
            }
        }
    };

    public static readonly AppRole[] AppRoles = new[]
    {
        new AppRole
        {
            Name = "Editor",
            Subjects = new []
            {
                "__add__auth0__"
            }
        },
        new AppRole
        {
            Name = "Admin",
            Subjects = new []
            {
                "__add__okta__"
            }
        },
        new AppRole
        {
            Name = "Reader"
        }
    };
}