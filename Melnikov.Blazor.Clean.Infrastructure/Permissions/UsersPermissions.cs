using System.ComponentModel;
using Melnikov.Blazor.Clean.Application.Common.Permissions;

namespace Melnikov.Blazor.Clean.Infrastructure.Permissions;

[DisplayName("Users")]
[Description("Users Permissions")]
public class UsersPermissions : PermissionBase
{
    public const string View = "Permissions.Users.View";
    public const string Create = "Permissions.Users.Create";
    public const string Edit = "Permissions.Users.Edit";
    public const string Delete = "Permissions.Users.Delete";
    public const string Search = "Permissions.Users.Search";
    public const string Import = "Permissions.Users.Import";
    public const string Export = "Permissions.Users.Export";
    public const string ManageRoles = "Permissions.Users.ManageRoles";
    public const string RestPassword = "Permissions.Users.RestPassword";
    public const string Active = "Permissions.Users.Active";
    public const string ManagePermissions = "Permissions.Users.Permissions";
}