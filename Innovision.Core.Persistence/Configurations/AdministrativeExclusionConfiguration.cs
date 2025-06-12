using Innovision.Core.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Innovision.Core.Persistence.Configurations;

public class AdministrativeExclusionConfiguration : IEntityTypeConfiguration<AdministrativeExclusion>
{
    public void Configure(EntityTypeBuilder<AdministrativeExclusion> builder)
    {
        builder.ToTable("AdministrativeExclusion");
        builder.HasKey(e => e.AdministrativeExclusionId);

        builder.Property(o => o.AdministrativeExclusionId)
            .UseIdentityColumn(1, 1);

        builder.HasOne(e => e.Account)
            .WithMany(f => f.AdministrativeExclusions)
            .HasForeignKey(e => e.AccountId);
    }
}
