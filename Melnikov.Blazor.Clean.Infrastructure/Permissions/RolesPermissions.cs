using System.ComponentModel;
using Melnikov.Blazor.Clean.Application.Common.Permissions;

namespace Melnikov.Blazor.Clean.Infrastructure.Permissions;

[DisplayName("Roles")]
[Description("Roles Permissions")]
public class RolesPermissions : PermissionBase
{
    public const string View = "Permissions.Roles.View";
    public const string Create = "Permissions.Roles.Create";
    public const string Edit = "Permissions.Roles.Edit";
    public const string Delete = "Permissions.Roles.Delete";
    public const string Search = "Permissions.Roles.Search";
    public const string Export = "Permissions.Roles.Export";
    public const string Import = "Permissions.Roles.Import";
    public const string ManagePermissions = "Permissions.Roles.Permissions";
    public const string ManageNavigation = "Permissions.Roles.Navigation";
}