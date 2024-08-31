using Melnikov.Blazor.Clean.Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Melnikov.Blazor.Clean.Infrastructure.Persistence.Configurations.Identity;

public class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
{
    public void Configure(EntityTypeBuilder<ApplicationRole> builder)
    {
        // Each Role can have many RoleClaims
        builder.HasMany(e => e.RoleClaims)
            .WithOne(e => e.Role)
            .HasForeignKey(rc => rc.RoleId)
            .IsRequired();

        builder.HasMany(x => x.Users)
            .WithMany(x => x.Roles)
            .UsingEntity<ApplicationUserRole>(
                l => l.HasOne<ApplicationUser>(e => e.User).WithMany(e => e.UserRoles).HasForeignKey(e => e.UserId),
                r => r.HasOne<ApplicationRole>(e => e.Role).WithMany(e => e.UserRoles).HasForeignKey(e => e.RoleId));

        builder.HasOne(x => x.Tenant)
            .WithMany()
            .HasForeignKey(u => u.TenantId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
    }
}