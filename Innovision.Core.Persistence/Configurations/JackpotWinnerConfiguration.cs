using Innovision.Core.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Innovision.Core.Persistence.Configurations;

public class JackpotWinnerConfiguration : IEntityTypeConfiguration<JackpotWinner>
{
    public void Configure(EntityTypeBuilder<JackpotWinner> builder)
    {
        builder.ToTable("JackpotWinner");
        builder.HasKey(e => e.JackpotWinnerId);

        builder.Property(e => e.JackpotWinnerId)
            .UseIdentityColumn(1, 1);

        builder.Property(e => e.Remarks)
            .IsRequired(false);

        builder.HasOne(e => e.Game)
            .WithMany(f => f.JackpotWinners)
            .HasForeignKey(f => f.GameId);

        builder.HasOne(e => e.GameType)
            .WithMany(f => f.JackpotWinners)
            .HasForeignKey(f => f.GameTypeId);

        builder.HasOne(e => e.Account)
            .WithMany(f => f.JackpotWinners)
            .HasForeignKey(f => f.AccountInfoId);

        builder.HasOne(e => e.ApproverAccount)
            .WithMany(f => f.ApprovedJackpotWinners)
            .HasForeignKey(f => f.ApproverAccountId);

        builder.HasOne(e => e.ReleaserAccount)
            .WithMany(f => f.ReleasedJackpotWinners)
            .HasForeignKey(f => f.ReleaserAccountId);

        builder.HasOne(e => e.OrderItem)
            .WithOne(f => f.JackpotWinner)
            .HasForeignKey<JackpotWinner>(f => f.OrderItemId);

    }
}
