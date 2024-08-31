using System.Reflection;
using Melnikov.Blazor.Clean.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Melnikov.Blazor.Clean.Infrastructure.Persistence;

public class ApplicationDbContext(DbContextOptions options) : IdentityDbContext<
    ApplicationUser, ApplicationRole, int,
    ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin,
    ApplicationRoleClaim, ApplicationUserToken>(options)
{
    public DbSet<ApplicationTenant> Tenants { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}