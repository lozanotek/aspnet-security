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
                //"__add__auth0__"
                "auth0|62a7c0979dc71e396ce57392"
            }
        },
        new AppRole
        {
            Name = "Admin",
            Subjects = new []
            {
                //"__add__okta__"
                "okta.demo@lozanotek.com"
            }
        },
        new AppRole
        {
            Name = "Reader"
        }
    };
}