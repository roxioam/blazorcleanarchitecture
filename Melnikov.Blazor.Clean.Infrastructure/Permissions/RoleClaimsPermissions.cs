using System.ComponentModel;
using Melnikov.Blazor.Clean.Application.Common.Permissions;

namespace Melnikov.Blazor.Clean.Infrastructure.Permissions;

[DisplayName("Role Claims")]
[Description("Role Claims Permissions")]
public class RoleClaimsPermissions : PermissionBase
{
    public const string View = "Permissions.RoleClaims.View";
    public const string Create = "Permissions.RoleClaims.Create";
    public const string Edit = "Permissions.RoleClaims.Edit";
    public const string Delete = "Permissions.RoleClaims.Delete";
    public const string Search = "Permissions.RoleClaims.Search";
}