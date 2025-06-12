using Innovision.Core.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Innovision.Core.Persistence.Configurations;

public class WithdrawalConfiguration : IEntityTypeConfiguration<Withdrawal>
{
    public void Configure(EntityTypeBuilder<Withdrawal> builder)
    {
        builder.ToTable("Withdrawal");
        builder.HasKey(e => e.TransactionId);

        builder.Property(e => e.TransactionNo)
            .HasMaxLength(50)
            .IsRequired(false);

        builder.Property(e => e.AccountInfoId)
            .IsRequired();

        builder.Property(e => e.TransactionDate)
            .IsRequired();

        builder.Property(e => e.CreatedOn)
            .IsRequired();

        builder.Property(e => e.Remarks)
            .IsRequired(false);

        builder.Property(e => e.CreatedBy)
            .HasMaxLength(100);

        builder.Property(e => e.LastModified);

        builder.Property(e => e.ModifiedBy)
            .HasMaxLength(100);

        builder.Property(e => e.Amount)
            .HasColumnType("decimal(18, 4)");

        builder.Property(e => e.NotificationStatus)
            .HasDefaultValue(-1);

        builder.HasOne(e => e.AccountInfo)
            .WithMany(f => f.Withdrawals)
            .HasForeignKey(e => e.AccountInfoId);

        builder.HasOne(e => e.WithdrawalStatus)
            .WithMany(f => f.Withdrawals)
            .HasForeignKey(e => e.Status);

        builder.HasOne(e => e.BankReference)
            .WithMany(f => f.Withdrawals)
            .HasForeignKey(e => e.BankReferenceId);
    }
}
