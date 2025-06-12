using Innovision.Core.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Innovision.Core.Persistence.Configurations;

public class DepositConfiguration : IEntityTypeConfiguration<Deposit>
{
    public void Configure(EntityTypeBuilder<Deposit> builder)
    {
        builder.ToTable("Deposit");
        builder.HasKey(e => e.DepositId);

        builder.Property(e => e.DepositId)
            .UseIdentityColumn(1, 1);

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

        builder.HasOne(e => e.AccountInfo)
            .WithMany(f => f.Deposits)
            .HasForeignKey(e => e.AccountInfoId);

        builder.HasOne(e => e.PaymentMethod)
            .WithMany(f => f.Deposits)
            .HasForeignKey(e => e.PaymentMethodId);

        builder.HasOne(e => e.DepositStatus)
            .WithMany(f => f.Deposits)
            .HasForeignKey(e => e.DepositStatusId);
    }
}
