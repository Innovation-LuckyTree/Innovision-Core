using Innovision.Core.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Innovision.Core.Persistence.Configurations;

public class SelfLimitConfiguration : IEntityTypeConfiguration<SelfLimit>
{
    public void Configure(EntityTypeBuilder<SelfLimit> builder)
    {
        builder.ToTable("SelfLimit");
        builder.HasKey(e => e.SelfLimitId);

        builder.Property(o => o.SelfLimitId)
            .UseIdentityColumn(1, 1);

        builder.HasOne(e => e.Account)
            .WithMany(f => f.SelfLimits)
            .HasForeignKey(e => e.AccountId);
    }
}
