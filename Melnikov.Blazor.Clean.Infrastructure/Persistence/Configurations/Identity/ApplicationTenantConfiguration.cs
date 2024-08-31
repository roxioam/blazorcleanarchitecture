using Melnikov.Blazor.Clean.Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Melnikov.Blazor.Clean.Infrastructure.Persistence.Configurations.Identity;

public class ApplicationTenantConfiguration : IEntityTypeConfiguration<ApplicationTenant>
{
    public void Configure(EntityTypeBuilder<ApplicationTenant> builder)
    {
        builder.Property(e => e.Name)
            .HasMaxLength(50)
            .IsUnicode()
            .IsRequired();

        builder.HasIndex(e => e.Name)
            .IsUnique();

        builder.Property(e => e.Description)
            .IsUnicode()
            .HasMaxLength(200);
    }
}