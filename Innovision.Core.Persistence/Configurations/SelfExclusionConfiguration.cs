using Innovision.Core.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Innovision.Core.Persistence.Configurations;

public class SelfExclusionConfiguration : IEntityTypeConfiguration<SelfExclusion>
{
    public void Configure(EntityTypeBuilder<SelfExclusion> builder)
    {
        builder.ToTable("SelfExclusion");
        builder.HasKey(e => e.SelfExclusionId);

        builder.Property(o => o.SelfExclusionId)
            .UseIdentityColumn(1, 1);

        builder.HasOne(e => e.Account)
            .WithMany(f => f.SelfExclusions)
            .HasForeignKey(e => e.AccountId);
    }
}