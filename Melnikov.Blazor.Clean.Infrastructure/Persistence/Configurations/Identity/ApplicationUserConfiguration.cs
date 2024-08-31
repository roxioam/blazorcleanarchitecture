using Melnikov.Blazor.Clean.Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Melnikov.Blazor.Clean.Infrastructure.Persistence.Configurations.Identity;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        // Each User can have many UserLogins
        builder.HasMany(e => e.UserClaims)
            .WithOne(e => e.User)
            .HasForeignKey(uc => uc.UserId)
            .IsRequired();

        // Each User can have many UserLogins
        builder.HasMany(e => e.UserLogins)
            .WithOne()
            .HasForeignKey(ul => ul.UserId)
            .IsRequired();

        // Each User can have many UserTokens
        builder.HasMany(e => e.UserTokens)
            .WithOne()
            .HasForeignKey(ut => ut.UserId)
            .IsRequired();

        builder.HasMany(x => x.Roles)
            .WithMany(x => x.Users)
            .UsingEntity<ApplicationUserRole>(
                l => l.HasOne<ApplicationRole>(e => e.Role).WithMany(e => e.UserRoles).HasForeignKey(e => e.RoleId),
                r => r.HasOne<ApplicationUser>(e => e.User).WithMany(e => e.UserRoles).HasForeignKey(e => e.UserId));
        
        builder.HasOne(x => x.Superior)
            .WithMany()
            .HasForeignKey(u => u.SuperiorId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(x => x.Tenant)
            .WithMany()
            .HasForeignKey(u => u.TenantId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
    }
}