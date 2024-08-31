using System.Security.Claims;
using Melnikov.Blazor.Clean.Application.Common.Permissions;
using Melnikov.Blazor.Clean.Domain.Identity;
using Melnikov.Blazor.Clean.Infrastructure.Constants.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Melnikov.Blazor.Clean.Infrastructure.Persistence;

public sealed class ApplicationDbContextInitializer(
    ILogger<ApplicationDbContextInitializer> logger,
    ApplicationDbContext context,
    UserManager<ApplicationUser> userManager,
    RoleManager<ApplicationRole> roleManager)
{
    public async Task InitialiseAsync()
    {
        try
        {
            await context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while initialising the database");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
            context.ChangeTracker.Clear();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while seeding the database");
            throw;
        }
    }

    private async Task TrySeedAsync()
    {
        // Default tenants
        if (!context.Tenants.Any())
        {
            context.Tenants.Add(new ApplicationTenant { Name = "Tenant 1", Description = "Tenant 1" });
            context.Tenants.Add(new ApplicationTenant { Name = "Tenant 2", Description = "Tenant 2" });

            await context.SaveChangesAsync();
        }

        var tenantId = context.Tenants.First().Id;

        // Default roles
        var administratorRole = new ApplicationRole
            { Name = RoleNameConstants.Administrator, Description = "Admin Group", TenantId = tenantId };

        var permissions = PermissionBase.GetAllPermissions();

        if (roleManager.Roles.All(r => r.Name != administratorRole.Name))
        {
            await roleManager.CreateAsync(administratorRole);

            foreach (var permission in permissions)
                await roleManager.AddClaimAsync(administratorRole,
                    new Claim(ApplicationClaimTypes.Permission, permission));
        }

        var userRole = new ApplicationRole
            { Name = RoleNameConstants.Basic, Description = "Basic Group", TenantId = tenantId };

        if (roleManager.Roles.All(r => r.Name != userRole.Name))
        {
            await roleManager.CreateAsync(userRole);

            foreach (var permission in permissions.Where(permission => permission.StartsWith("Permissions.Errors")))
                await roleManager.AddClaimAsync(userRole, new Claim(ApplicationClaimTypes.Permission, permission));
        }

        // Default users
        var administrator = new ApplicationUser
        {
            UserName = "admin@example.com",
            IsActive = true,
            TenantId = tenantId,
            FirstName = "Admin",
            LastName = "Example",
            Email = "admin@example.com",
            EmailConfirmed = true,
            TwoFactorEnabled = false
        };

        var demo = new ApplicationUser
        {
            UserName = "demo@example.com",
            IsActive = true,
            TenantId = tenantId,
            SuperiorId = 1,
            FirstName = "Demo",
            LastName = "Example",
            Email = "demo@example.com",
            EmailConfirmed = true,
            TwoFactorEnabled = false
        };

        if (userManager.Users.All(u => u.UserName != administrator.UserName))
        {
            await userManager.CreateAsync(administrator, UserNameConstants.DefaultPassword);
            await userManager.AddToRolesAsync(administrator, new[] { administratorRole.Name! });
        }

        if (userManager.Users.All(u => u.UserName != demo.UserName))
        {
            await userManager.CreateAsync(demo, UserNameConstants.DefaultPassword);
            await userManager.AddToRolesAsync(demo, new[] { userRole.Name! });
        }
    }
}