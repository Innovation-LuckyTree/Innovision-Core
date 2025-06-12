using Microsoft.EntityFrameworkCore;
using Innovision.Core.Domain.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Innovision.Core.Persistence.Configurations;

public class BlockedUserHistoryConfiguration : IEntityTypeConfiguration<BlockedUserHistory>
{
  public void Configure(EntityTypeBuilder<BlockedUserHistory> builder)
  {
    builder.ToTable("BlockedUserHistory");

    builder.HasKey(e => e.BlockedUserHistoryId);

    builder.Property(e => e.BlockedUserHistoryId)
        .UseIdentityColumn(1, 1);

    builder.HasOne(e => e.Account)
        .WithMany(f => f.BlockedUserHistories)
        .HasForeignKey(e => e.AccountInfoId);

    builder.Property(e => e.BlockedDate)
        .IsRequired();

    builder.Property(e => e.IsActive)
        .IsRequired()
        .HasDefaultValue(true);
  }
}
