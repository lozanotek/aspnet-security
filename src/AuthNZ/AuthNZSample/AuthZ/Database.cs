using AuthZ.Models;

namespace AuthZ;

public static class Database
{
	public static readonly Permission[] Permissions =
	[
		new Permission
		{
			Name = "CanEdit",
			AppRoles =
			[
				"Editor",
				"Admin"
			]
		},
		new Permission
		{
			Name = "CanSubmit",
			AppRoles =
			[
				"Admin"
			]
		},
		new Permission
		{
			Name = "CanRead",
			AppRoles =
			[
				"Admin",
				"Editor",
				"Reader"
			]
		}
	];

	public static readonly AppRole[] AppRoles =
	[
		new AppRole
		{
			Name = "Editor",
			Subjects =
			[
                //"__add__auth0__"
			]
		},
		new AppRole
		{
			Name = "Admin",
			Subjects =
			[
                //"__add__okta__"
			]
		},
		new AppRole
		{
			Name = "Reader"
		}
	];
}