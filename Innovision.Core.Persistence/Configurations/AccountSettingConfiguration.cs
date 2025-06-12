using Innovision.Core.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Innovision.Core.Persistence.Configurations
{
    public class AccountSettingConfiguration : IEntityTypeConfiguration<AccountSetting>
    {
        public void Configure(EntityTypeBuilder<AccountSetting> builder)
        {
            builder.ToTable("AccountSetting");
            builder.HasKey(e => e.AccountSettingId);
           
            builder.Property(e => e.AccountInfoId)
                .IsRequired();

            builder.Property(e => e.InAppNotification)
                .HasDefaultValue(false)
                .IsRequired();

            builder.Property(e => e.SmsNotification)
                .HasDefaultValue(false)
                .IsRequired();

            builder.Property(e => e.EmailNotification)
                .HasDefaultValue(false)
                .IsRequired();

            builder.HasOne(e => e.AccountInfo)
                .WithOne(f => f.AccountSetting)
                .HasForeignKey<AccountSetting>(e => e.AccountInfoId);
        }
    }
}
