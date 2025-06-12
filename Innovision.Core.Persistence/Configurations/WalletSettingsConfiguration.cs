using Innovision.Core.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Persistence.Configurations;

public class WalletSettingsConfiguration : IEntityTypeConfiguration<WalletSetting>
{
    public void Configure(EntityTypeBuilder<WalletSetting> builder)
    {
        builder.ToTable("WalletSetting");
        builder.HasKey(e => e.WalletSettingId);

        builder.Property(e => e.MaximumDepositAtOnce)
            .HasColumnName("MaximumDepositAtOnce");

        builder.Property(e => e.MaximumWithdrawAtOnce)
            .HasColumnName("MaximumWithdrawAtOnce");
    }
}
