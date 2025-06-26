using Innovision.Core.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Innovision.Core.Persistence.Configurations;

public class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.ToTable("Address");
        builder.HasKey(e => e.AddressId);

        builder.Property(o => o.AddressId)
            .UseIdentityColumn(1, 1);


        builder.Property(e => e.Region)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(e => e.Province)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(e => e.Municipality)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(e => e.Barangay)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(e => e.StreetOrPurok)
            .HasMaxLength(100)
            .IsRequired(false);
    }
}

