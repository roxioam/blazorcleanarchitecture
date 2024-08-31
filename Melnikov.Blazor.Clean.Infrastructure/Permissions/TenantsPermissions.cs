using System.ComponentModel;
using Melnikov.Blazor.Clean.Application.Common.Permissions;

namespace Melnikov.Blazor.Clean.Infrastructure.Permissions;

[DisplayName("Multi-Tenant")]
[Description("Multi-Tenant Permissions")]
public class TenantsPermissions : PermissionBase
{
    public const string View = "Permissions.Tenants.View";
    public const string Create = "Permissions.Tenants.Create";
    public const string Edit = "Permissions.Tenants.Edit";
    public const string Delete = "Permissions.Tenants.Delete";
    public const string Search = "Permissions.Tenants.Search";
}