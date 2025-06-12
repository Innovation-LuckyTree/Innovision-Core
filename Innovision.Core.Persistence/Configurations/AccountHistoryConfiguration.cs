using Innovision.Core.Domain.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Persistence.Configurations
{
    public class AccountHistoryConfiguration : IEntityTypeConfiguration<AccountHistory>
    {
        public void Configure(EntityTypeBuilder<AccountHistory> builder)
        {
            builder.ToTable("AccountHistory");
            builder.HasKey(e => e.AccountHistoryId);

            builder.Property(o => o.AccountHistoryId)
                .UseIdentityColumn(1, 1);

            builder.HasOne(e => e.Account)
                .WithMany(f => f.AccountHistories)
                .HasForeignKey(e => e.AccountInfoId);
        }
    }
}
