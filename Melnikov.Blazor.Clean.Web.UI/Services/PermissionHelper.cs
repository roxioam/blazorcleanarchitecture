using System.ComponentModel;
using System.Reflection;
using System.Security.Claims;
using Melnikov.Blazor.Clean.Application.Common.Exceptions;
using Melnikov.Blazor.Clean.Application.Common.Models.Security;
using Melnikov.Blazor.Clean.Application.Common.Permissions;
using Melnikov.Blazor.Clean.Domain.Identity;
using Melnikov.Blazor.Clean.Infrastructure.Constants.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;

namespace Melnikov.Blazor.Clean.Web.UI.Services;

public class PermissionHelper(
    RoleManager<ApplicationRole> roleManager,
    UserManager<ApplicationUser> userManager,
    IMemoryCache memoryCache)
{
    private readonly TimeSpan _refreshInterval = TimeSpan.FromDays(1);

    public async Task<List<PermissionModel>> GetAllPermissionsByUserId(int userId)
    {
        var assignedClaims = await GetUserClaimsByUserId(userId);
        var allPermissions = new List<PermissionModel>();
        var modules = PermissionBase.GetPermissionTypes();

        foreach (var module in modules)
        {
            var moduleName = module.GetCustomAttributes<DisplayNameAttribute>().FirstOrDefault()?.DisplayName ??
                             string.Empty;
            var moduleDescription = module.GetCustomAttributes<DescriptionAttribute>().FirstOrDefault()?.Description ??
                                    string.Empty;
            var fields = module.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);

            allPermissions.AddRange(fields.Select(field => field.GetValue(null)?.ToString())
                .Where(claimValue => claimValue != null)
                .Select(claimValue => new PermissionModel
                {
                    UserId = userId,
                    ClaimValue = claimValue ?? string.Empty,
                    ClaimType = ApplicationClaimTypes.Permission,
                    Group = moduleName,
                    Description = moduleDescription,
                    Assigned = assignedClaims != null && assignedClaims.Any(x => x.Value == claimValue)
                }));
        }

        return allPermissions;
    }

    private async Task<IList<Claim>?> GetUserClaimsByUserId(int userId)
    {
        var key = $"get-claims-by-{userId}";
        return await memoryCache.GetOrCreateAsync(key, async entry =>
        {
            entry.AbsoluteExpiration = DateTimeOffset.UtcNow.Add(_refreshInterval);
            var user = userManager.Users.SingleOrDefault(u => u.Id == userId) ??
                       throw new NotFoundException("User", userId);

            return await userManager.GetClaimsAsync(user);
        });
    }

    public async Task<List<PermissionModel>> GetAllPermissionsByRoleId(int roleId)
    {
        var assignedClaims = await GetUserClaimsByRoleId(roleId);
        var allPermissions = new List<PermissionModel>();
        var modules = PermissionBase.GetPermissionTypes();

        foreach (var module in modules)
        {
            var moduleName = module.GetCustomAttributes<DisplayNameAttribute>().FirstOrDefault()?.DisplayName ??
                             string.Empty;
            var moduleDescription = module.GetCustomAttributes<DescriptionAttribute>().FirstOrDefault()?.Description ??
                                    string.Empty;
            var fields = module.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);

            allPermissions.AddRange(fields.Select(field => field.GetValue(null)?.ToString())
                .Where(claimValue => !string.IsNullOrEmpty(claimValue))
                .Select(claimValue => new PermissionModel
                {
                    RoleId = roleId,
                    ClaimValue = claimValue ?? string.Empty,
                    ClaimType = ApplicationClaimTypes.Permission,
                    Group = moduleName,
                    Description = moduleDescription,
                    Assigned = assignedClaims != null && assignedClaims.Any(x => x.Value == claimValue)
                }));
        }

        return allPermissions;
    }

    private async Task<IList<Claim>?> GetUserClaimsByRoleId(int roleId)
    {
        var key = $"get-claims-by-{roleId}";
        return await memoryCache.GetOrCreateAsync(key, async entry =>
        {
            entry.AbsoluteExpiration = DateTimeOffset.UtcNow.Add(_refreshInterval);
            var role = roleManager.Roles.SingleOrDefault(u => u.Id == roleId) ??
                       throw new NotFoundException("Role", roleId);
            return await roleManager.GetClaimsAsync(role);
        });
    }
}