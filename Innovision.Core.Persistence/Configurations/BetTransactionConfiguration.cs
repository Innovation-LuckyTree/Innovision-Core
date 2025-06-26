using Innovision.Core.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Innovision.Core.Persistence.Configurations;

public class BetTransactionConfiguration : IEntityTypeConfiguration<BetTransaction>
{
    public void Configure(EntityTypeBuilder<BetTransaction> builder)
    {
        builder.ToTable("BetTransaction");
        builder.HasKey(e => e.BetTransactionId);

        builder.Property(e => e.AmountBet)
            .IsRequired();

        builder.Property(e => e.IsBonus)
            .HasDefaultValue(false);

        builder.Property(e => e.TransactionType)
            .HasDefaultValue("Regular");

        builder.HasOne(e => e.AccountInfo)
            .WithMany(f => f.BetTransactions)
            .HasForeignKey(e => e.AccountInfoId);

        builder.HasOne(e => e.DrawResult)
            .WithMany(f => f.BetTransactions)
            .HasForeignKey(e => e.BetTransactionId);

        builder.HasOne(e => e.Game)
            .WithMany(f => f.BetTransactions)
            .HasForeignKey(e => e.GameId);

        builder.HasOne(e => e.JackpotWinner)
            .WithOne(f => f.BetTransaction)
            .HasForeignKey<JackpotWinner>(e => e.BetTransactionId);
    }
}
